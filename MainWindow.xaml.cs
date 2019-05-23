using System;
using System.Collections.Generic;
using System.Windows;
using System.IO;
using Microsoft.Win32;
using System.Text;
using System.Reflection;


namespace ShowRoomCars
{
    public partial class MainWindow : Window
    {
        public int SelectedObject;

        private List<Trucks> _trucks = new List<Trucks>();
        private List<Cars> _cars = new List<Cars>();
        private List<Order> _orders = new List<Order>();

        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddAuto_Click(object sender, RoutedEventArgs e)
        {
            if (tbNumOrder.Text == "" || tbOrderDate.Text == "" || tbPaymentForm.Text == "")
            {
                MessageBox.Show("Please fill empty fields", "Showroow", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (rbLight.IsChecked == false && rbTruck.IsChecked == false)
            {
                MessageBox.Show("Please choose the car type", "Showroow", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }
            if (rbLight.IsChecked == true)
            {
                RightBox.IsEnabled = false;
                LeftBox.Visibility = Visibility.Hidden;
                GrAdditionLightAuto.Visibility = Visibility.Visible;
                GrAdditionLightAuto.Margin = new Thickness(10, 10, 0, 0);
            }
            else if (rbTruck.IsChecked == true)
            {
                RightBox.IsEnabled = false;
                LeftBox.Visibility = Visibility.Hidden;
                GrAddTrucks.Visibility = Visibility.Visible;
                GrAddTrucks.Margin = new Thickness(10, 10, 0, 0);
            }
        }

        private void AddCarAndReturn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Cars currentCar = new Cars(int.Parse(tbNumOrder.Text), cbBrandAuto.Text, tbYear.Text, cbColor.Text, int.Parse(tbPower.Text));
                _cars.Add(currentCar);
                MessageBox.Show("Car successfully added");

                cbBrandAuto.Text = "";
                tbYear.Clear();
                cbColor.Text = "";
                tbPower.Clear();
                RightBox.IsEnabled = true;
                GrAdditionLightAuto.Visibility = Visibility.Hidden;
                LeftBox.Visibility = Visibility.Visible;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddTruckAndReturn_Click(object sender, RoutedEventArgs e)
        {
            Trucks currentTruck = new Trucks(cbBrandHeavyAuto.Text, tbYearHeavy.Text, int.Parse(tbLoadCapacity.Text));
            _trucks.Add(currentTruck);
            MessageBox.Show("Truck successfully added");

            cbBrandHeavyAuto.Text = "";
            tbYearHeavy.Clear();
            tbLoadCapacity.Clear();
            RightBox.IsEnabled = true;
            GrAddTrucks.Visibility = Visibility.Hidden;
            LeftBox.Visibility = Visibility.Visible;
        }


        private void ShowAndEdit_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbListOrders.Text.Length != 0)
                {
                    SelectedObject = int.Parse(cbListOrders.SelectedValue.ToString());
                    for (int index = 0; index < _orders.Count; index++)
                    {
                        if (SelectedObject == _orders[index].NumOrder)
                        {
                            tbNumOrderToChange.Text = _orders[index].NumOrder.ToString();
                            tbPaymentFormToChange.Text = _orders[index].PaymentType;
                            tbDateToChange.Text = _orders[index].Date.ToString("dd.MM.yyyy");

                            List<Cars> cr = _orders[index].returnCars;
                            if (cr.Count != 0)
                            {
                                for (int ind = 0; ind < cr.Count; ind++)
                                {
                                    cbAutoToEdit.Items.Add(cr[ind].Brand);
                                }
                            }
                            List<Trucks> tr = _orders[index].returnTrucks;
                            if (tr.Count != 0)
                            {
                                for (int ind = 0; ind < tr.Count; ind++)
                                {
                                    cbAutoToEdit.Items.Add(tr[ind].Brand);
                                }
                            }
                            break;
                        }
                    }

                    LeftBox.Visibility = Visibility.Hidden;
                    RightBox.Visibility = Visibility.Hidden;
                    GrEditingOrder.Visibility = Visibility.Visible;
                    InformationAboutOrder.Visibility = Visibility.Visible;
                    GrEditingOrder.Margin = new Thickness(10, 10, 0, 0);
                    InformationAboutOrder.Margin = new Thickness(356, 10, 0, 0);
                }
                else
                {
                    MessageBox.Show("Choose order number.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonEditLightCar_Click(object sender, RoutedEventArgs e)
        {
            for (int index = 0; index < _orders.Count; index++)
            {
                if (SelectedObject == _orders[index].NumOrder)
                {
                    List<Cars> cr = _orders[index].returnCars;
                    for (int ind = 0; ind < cr.Count; ind++)
                    {
                        if (cr[ind].Brand == cbAutoToEdit.SelectedValue.ToString())
                        {
                            cr[ind].Brand = cbBrandAuto_Edit.Text;
                            cr[ind].Year = tbYear_Edit.Text;
                            cr[ind].Color = cbColor_Edit.Text;
                            cr[ind].Power = int.Parse(tbPower_Edit.Text);
                            cbAutoToEdit.Items.RemoveAt(cbAutoToEdit.SelectedIndex);
                            cbAutoToEdit.Items.Add(cr[ind].Brand);
                            MessageBox.Show("Car has successfully edited.");

                            cbBrandAuto_Edit.Text = "";
                            tbYear_Edit.Clear();
                            cbColor_Edit.Text = "";
                            tbPower_Edit.Clear();
                            break;
                        }
                    }
                }
            }
            GrEditLightCar.Visibility = Visibility.Hidden;
            GrEditingOrder.Visibility = Visibility.Visible;
            InformationAboutOrder.IsEnabled = true;
        }

        private void EditTrucks_Click(object sender, RoutedEventArgs e)
        {
            for (int index = 0; index < _orders.Count; index++)
            {
                if (SelectedObject == _orders[index].NumOrder)
                {
                    List<Trucks> tr = _orders[index].returnTrucks;
                    for (int ind = 0; ind < tr.Count; ind++)
                    {
                        if (tr[ind].Brand == cbAutoToEdit.SelectedValue.ToString())
                        {
                            tr[ind].Brand = cbBrandTruck_Edit.Text;
                            tr[ind].Year = tbYearHeavy_Edit.Text;
                            tr[ind].LoadCapacity = int.Parse(tbLoadCapacity_Edit.Text);
                            cbAutoToEdit.Items.RemoveAt(cbAutoToEdit.SelectedIndex);
                            cbAutoToEdit.Items.Add(tr[ind].Brand);
                            MessageBox.Show("Car has successfully edited.");

                            cbBrandTruck_Edit.Text = "";
                            tbYearHeavy_Edit.Clear();
                            tbLoadCapacity_Edit.Clear();
                            break;
                        }
                    }
                }
            }
            GrEditTrucks.Visibility = Visibility.Hidden;
            GrEditingOrder.Visibility = Visibility.Visible;
            InformationAboutOrder.IsEnabled = true;
        }

        private void EditAuto_Click(object sender, RoutedEventArgs e)
        {
            for (int index = 0; index < _orders.Count; index++)
            {
                if (SelectedObject == _orders[index].NumOrder)
                {
                    if (Enum.IsDefined(typeof(EBrands), cbAutoToEdit.Text))
                    {
                        GrEditingOrder.Visibility = Visibility.Hidden;
                        GrEditLightCar.Visibility = Visibility.Visible;
                        GrEditLightCar.Margin = new Thickness(10, 10, 0, 0);
                        InformationAboutOrder.IsEnabled = false;
                    }
                    else if (Enum.IsDefined(typeof(eBrandTrucks), cbAutoToEdit.Text))
                    {
                        GrEditingOrder.Visibility = Visibility.Hidden;
                        GrEditTrucks.Visibility = Visibility.Visible;
                        GrEditTrucks.Margin = new Thickness(10, 10, 0, 0);
                        InformationAboutOrder.IsEnabled = false;
                    }
                    break;
                }
            }
        }

        private void DeleteCarFromOrder_Click(object sender, RoutedEventArgs e)
        {
            for (int index = 0; index < _orders.Count; index++)
            {
                if (SelectedObject == _orders[index].NumOrder)
                {
                    if (Enum.IsDefined(typeof(EBrands), cbAutoToEdit.Text))
                    {
                        List<Cars> cr = _orders[index].returnCars;
                        for (int ind = 0; ind < cr.Count; ind++)
                        {
                            if (cr[ind].Brand == cbAutoToEdit.SelectedValue.ToString())
                            {
                                string deletedCar = cbAutoToEdit.SelectedValue.ToString();
                                cbAutoToEdit.Items.RemoveAt(cbAutoToEdit.SelectedIndex);
                                cr.Remove(cr[ind]);
                                MessageBox.Show("Car " + deletedCar + " removed from order.");
                                break;
                            }
                        }
                        _orders[index].returnCars = cr;
                        break;
                    }
                    else if (Enum.IsDefined(typeof(eBrandTrucks), cbAutoToEdit.Text))
                    {
                        List<Trucks> tr = _orders[index].returnTrucks;
                        for (int ind = 0; ind < tr.Count; ind++)
                        {
                            if (tr[ind].Brand == cbAutoToEdit.SelectedValue.ToString())
                            {
                                string deletedCar = cbAutoToEdit.SelectedValue.ToString();
                                cbAutoToEdit.Items.RemoveAt(cbAutoToEdit.SelectedIndex);
                                tr.Remove(tr[ind]);
                                MessageBox.Show("Car " + deletedCar + " removed from order.");
                                break;
                            }
                        }
                        _orders[index].returnTrucks = tr;
                        break;
                    }
                }
            }
        }

        private void buttonReturnToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            tbNumOrderToChange.Clear();
            tbPaymentFormToChange.Clear();
            tbDateToChange.Clear();
            cbAutoToEdit.Items.Clear();

            GrEditingOrder.Visibility = Visibility.Hidden;
            InformationAboutOrder.Visibility = Visibility.Hidden;
            LeftBox.Visibility = Visibility.Visible;
            RightBox.Visibility = Visibility.Visible;
        }

        private void AddOrder_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_cars.Count == 0 && _trucks.Count == 0)
                {
                    MessageBox.Show("Добавьте хотя бы один автомобиль к заказу", "ShowRoom", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    for (int index = 0; index < _orders.Count; index++)
                    {
                        if (_orders[index].NumOrder.Equals(int.Parse(tbNumOrder.Text)))
                        {
                            MessageBox.Show("Заказ с таким номером уже существует", "ShowRoom", MessageBoxButton.OK, MessageBoxImage.Information);
                            return;
                        }
                    }
                    Order currentOrder = new Order(int.Parse(tbNumOrder.Text), tbPaymentForm.Text, DateTime.Parse(tbOrderDate.Text), _cars, _trucks);
                    _orders.Add(currentOrder);
                    _cars.Clear();
                    _trucks.Clear();
                    cbListOrders.Items.Add(currentOrder.NumOrder);
                    MessageBox.Show("Order successfully added");

                    tbNumOrder.Clear();
                    tbOrderDate.Clear();
                    tbPaymentForm.Clear();
                    rbLight.IsChecked = false;
                    rbTruck.IsChecked = false;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void DeleteOrder_Click(object sender, RoutedEventArgs e)
        {
            if (cbListOrders.Text.Length != 0)
            {
                int numberToDelete = (int)cbListOrders.SelectedValue;
                for (int index = 0; index < _orders.Count; index++)
                {
                    if (numberToDelete == _orders[index].NumOrder)
                    {
                        _orders.Remove(_orders[index]);
                        cbListOrders.Items.RemoveAt(cbListOrders.SelectedIndex);
                        MessageBox.Show("Order number " + numberToDelete + " removed.");
                        break;
                    }
                }
            }
            else
            {
                MessageBox.Show("Choose order number.");
            }
        }


        private void UpdateInfo_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                int tempSelectedObject = SelectedObject;
                for (int currentOrder = 0; currentOrder < _orders.Count; currentOrder++)
                {
                    if (tempSelectedObject == _orders[currentOrder].NumOrder)
                    {
                        tempSelectedObject = currentOrder;
                        break;
                    }
                }
                InfoBox.Document.Blocks.Clear();
                Order ord = _orders[tempSelectedObject];
                InfoBox.AppendText("Номер заказа: " + ord.NumOrder + '\n' + "Тип оплаты: " + ord.PaymentType + '\n' +
                "Дата заказа: " + ord.Date.ToString("dd:MM:yyyy") + "\n \n");
                List<Cars> cr = ord.returnCars;
                List<Trucks> tr = ord.returnTrucks;
                InfoBox.AppendText("Автомобили: \n");
                if (cr.Count != 0)
                {
                    for (int index = 0; index < cr.Count; index++)
                    {
                        InfoBox.AppendText("Марка: " + cr[index].Brand + '\n' + "Год выпуска: " + cr[index].Year + '\n' +
                        "Цвет: " + cr[index].Color + '\n' + "Мощность: " + cr[index].Power + "\n \n");
                    }
                }
                if (tr.Count != 0)
                {
                    for (int index = 0; index < tr.Count; index++)
                    {
                        InfoBox.AppendText("Марка: " + tr[index].Brand + '\n' + "Год выпуска: " + tr[index].Year + '\n' +
                        "Грузоподъемность: " + tr[index].LoadCapacity + "\n \n");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonSaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                for (int index = 0; index < _orders.Count; index++)
                {
                    if (SelectedObject == _orders[index].NumOrder)
                    {
                        DateTime check;
                        if (DateTime.TryParse(tbDateToChange.Text, out check))
                        {
                            _orders[index].NumOrder = int.Parse(tbNumOrderToChange.Text);
                            _orders[index].PaymentType = tbPaymentFormToChange.Text;
                            _orders[index].Date = DateTime.Parse(tbDateToChange.Text);
                            cbListOrders.Items.RemoveAt(cbListOrders.SelectedIndex);
                            cbListOrders.Items.Add(_orders[index].NumOrder);
                            MessageBox.Show("Attributes successfully changed.");
                            break;
                        }
                        else
                        {
                            MessageBox.Show("Unable to convert date.");
                            return;
                        }
                    }
                }
                buttonReturnToMainMenu_Click(sender, e);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void SerializeObject_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbListOrders.Text.Length == 0)
                {
                    MessageBox.Show("Choose order.");
                }
                else
                {
                    int numberToSerialize = int.Parse(cbListOrders.SelectedValue.ToString());
                    for (int index = 0; index < _orders.Count; index++)
                    {
                        if (numberToSerialize == _orders[index].NumOrder)
                        {
                            SaveFileDialog dialog = new SaveFileDialog();
                            dialog.Filter = "XML file (*.xml)|*.xml|Binary data (*.bin)|*.bin|Json file (*.json)|*.json";
                            var result = dialog.ShowDialog();
                            if (result == null || result == false)
                            {
                                return;
                            }
                            var extension = Path.GetExtension(dialog.SafeFileName);
                            SerializeObject serialize = new SerializeObject(extension, dialog.FileName);
                            if (cypherType.Text.Length != 0)
                            {
                                serialize.ExecuteSerialization(_orders[index], cypherType.SelectedIndex);
                            }
                            else
                            {
                                serialize.ExecuteSerialization(_orders[index]);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Deserialize_Click(object sender, RoutedEventArgs e)
        {
            
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "XML file (*.xml)|*.xml|Binary data (*.bin)|*.bin|Json file (*.json)|*.json";
            var result = dlg.ShowDialog();
            if (result == null || result == false)
            {
                return;
            }
            var extension = Path.GetExtension(dlg.SafeFileName);
            SerializeObject serialize = new SerializeObject(extension, dlg.FileName);
            Order newOrder = null;
            try { newOrder = (Order)serialize.ExecuteDeserialization(); }
            catch (Exception)
            {
                try { newOrder = (Order)serialize.ExecuteDeserialization(0); }
                catch(Exception) { newOrder = (Order)serialize.ExecuteDeserialization(1); }
            }
            finally
            {
                _orders.Add(newOrder);
                cbListOrders.Items.Add(newOrder.NumOrder);
            }
        }
    }
}
