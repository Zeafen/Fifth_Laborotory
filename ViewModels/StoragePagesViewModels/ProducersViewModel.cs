using Final_Practice.Commands;
using Final_Practice.database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Final_Practice.ViewModels.StoragePagesViewModels
{
    public class ProducersViewModel : INotifyPropertyChanged
    {
        private RelayCommand<Producers> _addProducerCommand = null;
        public RelayCommand<Producers> addProducerCommand => _addProducerCommand ?? (_addProducerCommand = new RelayCommand<Producers>(AddProducer, (teaType) => !string.IsNullOrEmpty(teaType?.Producer_Name) && Producers.FirstOrDefault(r => r.Producer_Name == teaType?.Producer_Name) == null));

        private RelayCommand<Producers> _deleteProducerCommand = null;
        public RelayCommand<Producers> deleteProducerCommand => _deleteProducerCommand ?? (_deleteProducerCommand = new RelayCommand<Producers>(DeleteProducer, (teaType) => teaType != null && Producers.FirstOrDefault(p => p.ID_Producer == teaType?.ID_Producer) != null));

        private RelayCommand<Producers> _updateProducerCommand = null;
        public RelayCommand<Producers> updateProducerCommand => _updateProducerCommand ?? (_updateProducerCommand = new RelayCommand<Producers>(UpdateProducer, (teaType) => teaType != null && Producers.FirstOrDefault(p => p.ID_Producer == teaType?.ID_Producer) != null));


        private TeaShopDBContext ctx;

        public ObservableCollection<Producers> Producers { get; set; }

        private Producers _CurrentProducer { get; set; } = new Producers();
        public Producers CurrentProducer
        {
            get => _CurrentProducer;
            set
            {
                _CurrentProducer = value;
                OnPropertyChanged();
            }
        }

        public ProducersViewModel()
        {
            ctx = new TeaShopDBContext();
            Producers = new ObservableCollection<Producers>(ctx.Producers.ToList());
        }


        public void ClearInfo()
        {
            CurrentProducer = new Producers();
        }
        public void AddProducer(Producers newProducer)
        {
            try
            {
                if (newProducer == null) throw new ArgumentNullException("Cannot add null value");
                ctx.Producers.Add(newProducer);
                ctx.SaveChanges();
                Producers.Add(newProducer);
                CurrentProducer = new Producers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void UpdateProducer(Producers producer)
        {
            try
            {
                if (producer == null) throw new ArgumentNullException("Cannot add null value");
                var updatedProducer = ctx.Producers.FirstOrDefault(p => p.ID_Producer == producer.ID_Producer);
                var arrayUpdatedProducer = Producers.FirstOrDefault(p => p.ID_Producer == producer.ID_Producer);
                if (updatedProducer != null)
                {
                    updatedProducer = producer;
                    ctx.SaveChanges();
                    arrayUpdatedProducer = producer;
                }
                CurrentProducer = new Producers();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        public void DeleteProducer(Producers producer)
        {
            try
            {
                if (producer == null) throw new ArgumentNullException("Cannot add null value");
                foreach(var tea in ctx.TeaSorts)
                {
                    if (ctx.TeaSorts.FirstOrDefault(t => t.Producer_ID == producer.ID_Producer) != null)
                        ctx.TeaSorts.FirstOrDefault(t => t.Producer_ID == producer.ID_Producer).Producer_ID = null;
                }
                ctx.Producers.Remove(producer);
                ctx.SaveChanges();
                Producers.Remove(producer);
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
