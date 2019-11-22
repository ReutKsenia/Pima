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

namespace Pima.View.UserControls
{
    /// <summary>
    /// Логика взаимодействия для ArticleCard.xaml
    /// </summary>
    public partial class ArticleCard : UserControl
    {
        public ArticleCard()
        {
            InitializeComponent();
        }

        public event RoutedEventHandler OpenArticleMouseClick;

        private void OpenArticle_Click(object sender, RoutedEventArgs e)
        {
            if (OpenArticleMouseClick != null)
                OpenArticleMouseClick(sender, e);
        }
    }
}
