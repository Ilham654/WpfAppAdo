using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfAppAdo
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;
            _libraryContext = new();
        }

        public Category Category { get; set; }

        public Author Author { get; set; }

        private readonly LibraryContext _libraryContext;

        public List<Author> Authors
        {
            get { return (List<Author>)GetValue (AuthorsProperty); }
            set { SetValue(AuthorsProperty, value); }
        }

        // Using a DependencyProperty as the backing store for List<Author> Authors.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty AuthorsProperty =
            DependencyProperty.Register("Authors", typeof(List<Author>), typeof(MainWindow));

        public List<Book> Books
        {
            get { return (List<Book>)GetValue(BooksProperty); }
            set { SetValue(BooksProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty BooksProperty =
            DependencyProperty.Register("books", typeof(List<Book>), typeof(MainWindow));

        public List<Category> Categories             
        {
            get { return (List<Category>)GetValue(CategoriesProperty); }
            set { SetValue(CategoriesProperty, value); }
        }

        // Using a DependencyProperty as the backing store for MyProperty.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty CategoriesProperty =
            DependencyProperty.Register("Categories", typeof(List<Category>), typeof(MainWindow));

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                Authors = _libraryContext.Authors.ToList();
                Categories = _libraryContext.Categories.ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Books = _libraryContext.Books
                .Where(b => Author != null || b.IdAuthor == Author.Id && (Category == null || b.IdCategory 
                  == Category.Id)).ToList();
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }
    }
}
