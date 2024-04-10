using Final_Practice.Commands;
using Final_Practice.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity;
using System.Data.Entity.Migrations.Model;
using System.IO;
using System.IO.Packaging;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Final_Practice.ViewModels.CashierPagesViewModels
{
    public class CashierPageViewModel : INotifyPropertyChanged
    {
        private RelayCommand<decimal> _payOrderCommand = null;
        public RelayCommand<decimal> payOrderCommand => _payOrderCommand ?? (_payOrderCommand = new RelayCommand<decimal>(PayOrder));

        private RelayCommand<TeasMenu> _addTeaToOrderCommand = null;
        public RelayCommand<TeasMenu> addTeaToOrderCommand => _addTeaToOrderCommand ?? (_addTeaToOrderCommand = new RelayCommand<TeasMenu>(AddToOrder));
        private RelayCommand<CollectionsMenu> _addCollectionToOrderCommand = null;
        public RelayCommand<CollectionsMenu> addCollectionToOrderCommand => _addCollectionToOrderCommand ?? (_addCollectionToOrderCommand = new RelayCommand<CollectionsMenu>(AddToOrder));
        

        private bool _NeedPay {  get; set; }
        public bool NeedPay
        {
            get => _NeedPay; private set
            {
                _NeedPay = value;
                OnPropertyChanged();
            }
        }

        private TeaShopDBContext ctx;
        private readonly Employees CurrentUser = Application.Current.Resources["Current_Account"] as Employees;

        public TeasMenu CurrentTeasMenu
        {
            get => _CurrentTeasMenu;
            set
            {
                _CurrentTeasMenu = value;
                OnPropertyChanged();
            }
        }
        private TeasMenu _CurrentTeasMenu { get; set; } = new TeasMenu();

        public CollectionsMenu CurrentCollectionsMenu {
            get => _CurrentCollectionsMenu;
            set
            {
                _CurrentCollectionsMenu = value;
                OnPropertyChanged();
            }
        }
        private CollectionsMenu _CurrentCollectionsMenu { get; set; } = new CollectionsMenu();

        public Packages CurrentPackage
        {
            get => _CurrentPackage;
            set
            {
                _CurrentPackage = value;
                OnPropertyChanged();
            }
        }
        private Packages _CurrentPackage { get; set; }


        public Cups CurrentCup
        {
            get => _CurrentCup;
            set
            {
                _CurrentCup = value;
                OnPropertyChanged();
            }
        }
        private Cups _CurrentCup { get; set; }


        public OrderType CurrentOrderType { get; set; } = OrderType.Package;
        public Orders NewOrder { get; private set; } = null;
        public int AddingAmount { get; set; } = 1;

        public decimal CurrentPaid
        {
            get => _CurrentPaid;
            set
            {
                _CurrentPaid = value;
                OnPropertyChanged();
            }
        }
        private decimal _CurrentPaid { get; set; } = 0;

        public decimal CurrentTotalPrice { get; private set; } = 0;

        public ObservableCollection<TeasMenu> TeasMenu {  get; set; }
        public ObservableCollection<CollectionsMenu> CollectionsMenu {  get; set; }
        public ObservableCollection<Packages> Packages { get; set; }
        public ObservableCollection<Cups> Cups { get; set; }

        public CashierPageViewModel()
        {
            ctx = new TeaShopDBContext();
            TeasMenu = new ObservableCollection<TeasMenu>(ctx.TeasMenu.ToList());
            CollectionsMenu = new ObservableCollection<CollectionsMenu>(ctx.CollectionsMenu.ToList());
            Cups = new ObservableCollection<Cups>(ctx.Cups.ToList());
            Packages = new ObservableCollection<Packages>(ctx.Packages.ToList());
            AddNewOrder();
        }

        public bool CheckIfNewOrderExists()
        {
            if (ctx.Orders.FirstOrDefault(o => o.ID_Order == NewOrder.ID_Order) == null)
                return false;
            return true;
        }

        public void AddNewOrder()
        {
            NewOrder = new Orders { Order_TotalPrice = 0, Order_Date = DateTime.Now, Emplyoee_ID = CurrentUser?.ID_Employee };
            ctx.Orders.Add(NewOrder);
            ctx.SaveChanges();
            NeedPay = true;
        }

        public void SaveTheReceipt()
        {
            using (StreamWriter writer = new StreamWriter(File.Create(Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + $@"/Receipts{NewOrder.ID_Order}.txt")))
            {
                var res = new StringBuilder();
                res.AppendLine("\t\t<Final_Practice>");
                res.AppendLine($"\t    Кассовый чек N {NewOrder.ID_Order}");
                foreach (var item in NewOrder.Pack_OrdersTeas)
                {
                    var package = ctx.Packages.FirstOrDefault(p => p.ID_Package == item.Package_ID);
                    var tea = ctx.TeaSorts.FirstOrDefault(t => t.ID_TeaSort == item.TeaSort_ID);
                    res.AppendLine($"   ({package.Package_Name} - {package.Package_AmountTea}gr) {tea.TeaSort_Name}\t-\t{item.PackOrderTeas_Count} * {tea.TeaSort_PackPrice / 100 * package.Package_AmountTea}");
                }
                foreach (var item in NewOrder.Cup_OrdersTeas)
                {
                    var cup = ctx.Cups.FirstOrDefault(c => c.ID_Cup == item.Cup_ID);
                    var tea = ctx.TeaSorts.FirstOrDefault(t => t.ID_TeaSort == item.TeaSort_ID);
                    res.AppendLine($"\t({cup.Cup_Name} - {cup.Cup_AmountTea}gr) {tea.TeaSort_Name}\t-\t{item.CupOrderTeas_Count} * {tea.TeaSort_PackPrice / 50 * cup.Cup_AmountTea}");
                }
                foreach (var item in NewOrder.Collection_OrdersTeas)
                {
                    var collection = ctx.Collections.FirstOrDefault(c => c.ID_Collection == item.Collection_ID);
                    res.AppendLine($"\t{collection.Collection_Name}\t-\t{item.CollectionOrderTea_Count} * {collection.Collection_Price} \n\t(");
                    foreach (var collectionTea in collection.CollectionTeas)
                    {
                        res.AppendLine($"\t\t{collectionTea.TeaSorts.TeaSort_Name}\t-\t({collectionTea.Collections.CollectionForms.Collection_Size} * {collectionTea.CollectionTeas_AmountTeas})");
                    }
                    res.AppendLine("\t)");
                }
                res.AppendLine($"    Итого к оплате: {CurrentTotalPrice} $");
                res.AppendLine($"    Внесено: {NewOrder.Order_Paid} $");
                res.AppendLine($"    Сдача: {NewOrder.Order_PayBack} $");
                writer.Write(res.ToString());
            }
        }

        public void PayOrder(decimal paidSumm)
        {
            try
            {
                if (paidSumm < CurrentTotalPrice) throw new Exception("Not enough money to pay for all the teas");
                if (NewOrder.Order_TotalPrice > 0)
                {
                    NewOrder.Order_Paid = paidSumm;
                    NewOrder.Order_PayBack = paidSumm - CurrentTotalPrice;
                }
                var order = ctx.Orders.FirstOrDefault(o => o.ID_Order == NewOrder.ID_Order);
                order.Order_Paid = paidSumm;
                order.Order_PayBack = paidSumm - CurrentTotalPrice;
                ctx.SaveChanges();
                NeedPay = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                SaveTheReceipt();
            }
        }

        private void UpdateOrder()
        {
            try
            {
                CurrentTotalPrice = 0;
                foreach (var item in NewOrder.Pack_OrdersTeas)
                {
                    var package = ctx.Packages.FirstOrDefault(p => p.ID_Package == item.Package_ID);
                    var tea = ctx.TeaSorts.FirstOrDefault(t => t.ID_TeaSort == item.TeaSort_ID);
                    if (tea != null && package != null)
                        CurrentTotalPrice += item.PackOrderTeas_Count * package.Package_AmountTea * tea.TeaSort_PackPrice / 100;
                }
                foreach (var item in NewOrder.Cup_OrdersTeas)
                {
                    var cup = ctx.Cups.FirstOrDefault(c => c.ID_Cup == item.Cup_ID);
                    var tea = ctx.TeaSorts.FirstOrDefault(t => t.ID_TeaSort == item.TeaSort_ID);
                    if (tea != null && cup != null)
                        CurrentTotalPrice += item.CupOrderTeas_Count * cup.Cup_AmountTea * tea.TeaSort_CupPrice / 50;
                }
                foreach (var item in NewOrder.Collection_OrdersTeas)
                {
                    var collection = ctx.Collections.FirstOrDefault(c => c.ID_Collection == item.Collection_ID);
                    if (collection != null)
                        CurrentTotalPrice += collection.Collection_Price * item.CollectionOrderTea_Count;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public void AddToOrder(TeasMenu teasMenu)
        {
            if (teasMenu == null)
                return;
            try
            {
                switch(CurrentOrderType)
                {
                    case OrderType.Package:
                        if (CurrentPackage == null) throw new ArgumentNullException(nameof(CurrentPackage),"Cannot create package order with no selected package");
                        else if(ctx.Pack_OrdersTeas.FirstOrDefault(po => po.Order_ID == NewOrder.ID_Order && po.TeaSort_ID == teasMenu.ID && po.Package_ID == CurrentPackage.ID_Package) != null)
                            throw new Exception("Order already contains this good, if oyu want to change count, go to order`s configuration page");
                        var newPackageOrder = new Pack_OrdersTeas() { Order_ID = NewOrder.ID_Order, Package_ID = CurrentPackage.ID_Package, TeaSort_ID = teasMenu.ID, PackOrderTeas_Count = AddingAmount };
                        ctx.Pack_OrdersTeas.Add(newPackageOrder);
                        ctx.SaveChanges();
                        break;
                    case OrderType.Cup:
                        if (CurrentCup == null) throw new ArgumentNullException(nameof(CurrentCup),"Cannot create cup order with no selected cup");
                        else if(ctx.Cup_OrdersTeas.FirstOrDefault(po => po.Order_ID == NewOrder.ID_Order && po.TeaSort_ID == teasMenu.ID && po.Cup_ID == CurrentCup.ID_Cup) != null)
                            throw new Exception("Order already contains this good, if oyu want to change count, go to order`s configuration page");
                        var newCupOrder = new Cup_OrdersTeas() { Order_ID = NewOrder.ID_Order, Cup_ID = CurrentCup.ID_Cup, TeaSort_ID = teasMenu.ID, CupOrderTeas_Count = AddingAmount };
                        ctx.Cup_OrdersTeas.Add(newCupOrder);
                        ctx.SaveChanges();
                        break;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Not enough tea at storage or {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateOrder();
            }
        }

        public void AddToOrder(CollectionsMenu collectionMenu)
        {
            try
            {
                if (collectionMenu == null) throw new ArgumentNullException(nameof(collectionMenu), "Cannot ad to order empty query");
                var newCollection = new Collection_OrdersTeas() { Collection_ID = collectionMenu.ID, Order_ID = NewOrder.ID_Order, CollectionOrderTea_Count = AddingAmount };
                ctx.Collection_OrdersTeas.Add(newCollection);
                ctx.SaveChanges();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Not enough tea at storage or {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                UpdateOrder();
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
