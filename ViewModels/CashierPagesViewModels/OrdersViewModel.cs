using Final_Practice.Commands;
using Final_Practice.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Data.Entity.Migrations.Model;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.CashierPagesViewModels
{
    public class OrdersViewModel : INotifyPropertyChanged
    {
        private RelayCommand<Orders> _uploadReceiptCommand = null;
        public RelayCommand<Orders> uploadReceiptCommand => _uploadReceiptCommand ?? (_uploadReceiptCommand = new RelayCommand<Orders>(UploadReceipt));

        private RelayCommand<Orders> _deleteOrderCommand = null;
        public RelayCommand<Orders> deleteOrderCommand => _deleteOrderCommand ?? (_deleteOrderCommand = new RelayCommand<Orders>(DeleteOrder, (order) => Orders.FirstOrDefault(c => c.ID_Order == order?.ID_Order) != null));

        private RelayCommand<Orders> _updateOrderCommand = null;
        public RelayCommand<Orders> updateOrderCommand => _updateOrderCommand ?? (_updateOrderCommand = new RelayCommand<Orders>(UpdateOrder, (order) => Orders.FirstOrDefault(c => c.ID_Order == order?.ID_Order) != null));


        private TeaShopDBContext ctx;
        public ObservableCollection<Orders> Orders { get; set; }

        private Orders _CurrentOrder { get; set; } = new Orders();
        public Orders CurrentOrder
        {
            get => _CurrentOrder;
            set
            {
                _CurrentOrder = value;
                OnPropertyChanged();
            }
        }

        public OrdersViewModel()
        {
            ctx = new TeaShopDBContext();
            Orders = new ObservableCollection<Orders>(ctx.Orders.ToList().Distinct());
        }

        private void UploadReceipt(Orders order)
        {
            var path = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + $@"/Receipts{order.ID_Order}.txt";
            if (File.Exists(path))
                File.Delete(path);
            using (StreamWriter writer = new StreamWriter(File.Create(path)))
            {
                var res = new StringBuilder();
                res.AppendLine("\t\t<Final_Practice>");
                res.AppendLine($"\t    Кассовый чек N {order.ID_Order}");
                foreach (var item in order.Pack_OrdersTeas)
                {
                    var package = ctx.Packages.FirstOrDefault(p => p.ID_Package == item.Package_ID);
                    var tea = ctx.TeaSorts.FirstOrDefault(t => t.ID_TeaSort == item.TeaSort_ID);
                    res.AppendLine($"   ({package.Package_Name} - {package.Package_AmountTea}gr) {tea.TeaSort_Name}\t-\t{item.PackOrderTeas_Count} * {tea.TeaSort_PackPrice / 100 * package.Package_AmountTea}");
                }
                foreach (var item in order.Cup_OrdersTeas)
                {
                    var cup = ctx.Cups.FirstOrDefault(c => c.ID_Cup == item.Cup_ID);
                    var tea = ctx.TeaSorts.FirstOrDefault(t => t.ID_TeaSort == item.TeaSort_ID);
                    res.AppendLine($"\t({cup.Cup_Name} - {cup.Cup_AmountTea}gr) {tea.TeaSort_Name}\t-\t{item.CupOrderTeas_Count} * {tea.TeaSort_PackPrice / 50 * cup.Cup_AmountTea}");
                }
                foreach (var item in order.Collection_OrdersTeas)
                {
                    var collection = ctx.Collections.FirstOrDefault(c => c.ID_Collection == item.Collection_ID);
                    res.AppendLine($"\t{collection.Collection_Name}\t-\t{item.CollectionOrderTea_Count} * {collection.Collection_Price} \n\t(");
                    foreach (var collectionTea in collection.CollectionTeas)
                    {
                        res.AppendLine($"\t\t{collectionTea.TeaSorts.TeaSort_Name}\t-\t({collectionTea.Collections.CollectionForms.Collection_Size} * {collectionTea.CollectionTeas_AmountTeas})");
                    }
                    res.AppendLine("\t)");
                }
                res.AppendLine($"    Итого к оплате: {order.Order_TotalPrice} $");
                res.AppendLine($"    Внесено: {order.Order_Paid} $");
                res.AppendLine($"    Сдача: {order.Order_PayBack} $");
                writer.Write(res.ToString());
            }
        }

        public void UpdateOrderPayBack()
        {
            try
            {
                CurrentOrder.Order_PayBack = ctx.Orders.FirstOrDefault(o => o.ID_Order == CurrentOrder.ID_Order).Order_TotalPrice - CurrentOrder.Order_Paid;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void ClearInfo()
        {
            CurrentOrder = new Orders();
        }
        public void UpdateOrder(Orders Order)
        {
            try
            {
                if (Order == null) throw new ArgumentNullException("Cannot add null value");
                var updatedOrder = ctx.Orders.FirstOrDefault(c => c.ID_Order == Order.ID_Order);
                var updatedArrayOrder = Orders.FirstOrDefault(c => c.ID_Order == Order.ID_Order);
                if (updatedOrder != null)
                {
                    updatedOrder = Order;
                    ctx.SaveChanges();
                    updatedArrayOrder = Order;
                }
                CurrentOrder = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteOrder(Orders order)
        {
            try
            {
                if (order == null) throw new ArgumentNullException(nameof(order),"Cannot delete null value");
                var orderToDelete = ctx.Orders.FirstOrDefault(o => o.ID_Order ==  order.ID_Order);
                if (orderToDelete == null) throw new ArgumentException("This order doesn't exist in DB");
                ctx.Orders.Remove(orderToDelete);
                ctx.SaveChanges();
                Orders.Remove(orderToDelete);
                CurrentOrder = null;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
