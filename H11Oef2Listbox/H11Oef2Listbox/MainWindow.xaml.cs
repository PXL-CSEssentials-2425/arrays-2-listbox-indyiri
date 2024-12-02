using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace H11Oef2Listbox
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        string[] listItems = {"Indy", "Nuri", "Ingrid", "Simba", "Jamba", "Dori", "Chum"};

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (string lItems in listItems)
            { 
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = lItems;
                simpleListBox.Items.Add(listBoxItem);
            }
        }

        private void addButton_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(addTextBox.Text))
            {
                MessageBox.Show("Er is niets om toe te voegen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                ListBoxItem newItem = new ListBoxItem();
                newItem.Content = addTextBox.Text;
                simpleListBox.Items.Add(newItem);

                Array.Resize(ref listItems, listItems.Length + 1);
                listItems[listItems.Length - 1] = addTextBox.Text;
            }
        }

        private void replaceButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedItemIndex = simpleListBox.SelectedIndex;

            if (selectedItemIndex == -1)
            {
                MessageBox.Show("Er is geen item geselecteerd om te vervangen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                if (string.IsNullOrWhiteSpace(replaceTextBox.Text))
                {
                    MessageBox.Show("Er is geen input om het geselecteerde item mee te vervangen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    ListBoxItem replaceItem = new ListBoxItem();
                    replaceItem.Content = replaceTextBox.Text;

                    simpleListBox.Items[selectedItemIndex] = replaceItem;

                    listItems[selectedItemIndex] = replaceTextBox.Text;
                }  
            }  
        }

        private void searchButton_Click(object sender, RoutedEventArgs e)
        {
            int searchIndex = -1;

            if (simpleListBox.Items.Count == 0)
            {
                MessageBox.Show("Er zijn geen listItems om in te zoeken","Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else 
            {
                bool searchedItemExists = Array.Exists(listItems, item => item.Equals(searchTextBox.Text, StringComparison.OrdinalIgnoreCase));

                if (searchedItemExists == false)
                {
                    searchLabel.Content = "Gezochte item is niet aanwezig in de ListBox";
                }
                else if (searchedItemExists == true)
                {
                    for (int i = 0; i < simpleListBox.Items.Count; i++)
                    {
                        ListBoxItem item = simpleListBox.Items[i] as ListBoxItem;

                        if (item.Content.ToString().Equals(searchTextBox.Text, StringComparison.OrdinalIgnoreCase))
                        {
                            searchIndex = i;
                        }
                    }

                    simpleListBox.SelectedIndex = searchIndex;
                    searchLabel.Content = $"Het gezochte item bevindt zich op plaats {searchIndex + 1}";
                }
            }
        }

        private void clearButton_Click(object sender, RoutedEventArgs e)
        {
            simpleListBox.Items.Clear();

            listItems = new string[0];
        }

        private void deleteButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedItemIndex = simpleListBox.SelectedIndex;

            if (selectedItemIndex == -1)
            {
                MessageBox.Show("Er is geen item geselecteerd om te verwijderen", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                simpleListBox.Items.RemoveAt(selectedItemIndex);

                for (int i = selectedItemIndex; i < listItems.Length - 1; i++)
                {
                    listItems[i] = listItems[i + 1];
                }

                Array.Resize(ref listItems, listItems.Length - 1);
            }
        }

        private void sortButton_Click(object sender, RoutedEventArgs e)
        {
            Array.Sort(listItems);

            simpleListBox.Items.Clear();

            foreach (string lItems in listItems)
            {
                ListBoxItem listBoxItem = new ListBoxItem();
                listBoxItem.Content = lItems;
                simpleListBox.Items.Add(listBoxItem);
            }
        }
    }
}