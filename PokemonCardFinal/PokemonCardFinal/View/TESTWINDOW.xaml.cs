using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using System.Windows.Shapes;
using DataDomain;
using LogicLayer;
using LogicLayerInterfaces;

namespace PokemonCardFinal.View
{
    /// <summary>
    /// Interaction logic for TESTWINDOW.xaml
    /// </summary>
    public partial class TESTWINDOW : Window
    {
        public TESTWINDOW()
        {
            InitializeComponent();
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //TestElementType();
            TestArtist();
        }

        private void TestElementType()
        {
            ElementType elmType = null;


            try
            {
                ElementManager manager = new ElementManager();
                elmType = manager.GetElementTypeByElementTypeID("fire");
                List<ElementType> elmList = manager.GetElementTypes();

                foreach (ElementType element in elmList)
                {
                    lblName.Content += "\nElementTypeID: " + element.ElementTypeID;
                }

                if (elmType != null)
                {
                    //lblName.Content = elmType.ElementTypeID + elmType.Description;
                }
                else
                {
                    MessageBox.Show("Object is nul;");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        private void TestArtist() 
        {
            IArtistManager artistManager = new ArtistManager();

            try
            {
                //Artist artist = artistManager.GetArtistByArtistID(56);
                //lblName.Content = artist.GivenName + ", " + artist.Surname;

                //Artist artist = artistManager.GetArtistByName("fujimoto", "gold");
                //lblName.Content = artist.GivenName + ", " + artist.Surname;

                //List<Artist> artists = artistManager.GetArtists();
                //foreach (Artist artist in artists)
                //{
                //    lblName.Content += artist.GivenName + ", " + artist.Surname + "\n";
                //}

                //if (artistManager.AddArtist("test", "person"))
                //{
                //    MessageBox.Show("Artist added.");
                //}
                //else
                //{
                //    MessageBox.Show("Failed to add artist.");
                //}

                if (artistManager.EditArtistByArtistID(3, "Nothgin", ""))
                {
                    MessageBox.Show("Artist updated.");
                }
                else
                {
                    MessageBox.Show("Failed to upgggggggdate artist.");
                }

                //if (artistManager.DeleteArtistByArtistID(3))
                //{
                //    MessageBox.Show("Artist deleted.");
                //}
                //else
                //{
                //    MessageBox.Show("Failed to deleteggggd artist.");
                //}
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message + "\n");
            }
        }
    }
}
