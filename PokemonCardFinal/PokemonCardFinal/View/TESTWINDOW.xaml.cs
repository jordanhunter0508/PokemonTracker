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
            //TestArtist();
            //TestBooster();
            
        }

        private void TestBooster()
        {
            IBoosterManger boosterManager = new BoosterManager();

            //Booster booster = boosterManager.GetBoosterByBoosterID("151");

            //List<Booster> boosters = boosterManager.GetBoosters();

            //if (booster == null)
            //{
            //    MessageBox.Show("Booster is null");
            //    return;
            //}
            //lblName.Content = boosters.Count;

            //Booster newBooster = new Booster()
            //{
            //    BoosterID = "Meaga Evolution",
            //    Series = "Mega Evolution",
            //    ReleaseDate = DateTime.Parse("2025-05-04"),
            //    Abbreviation = "meg"
            //};

            //    if (boosterManager.AddBooster(newBooster))
            //    {
            //        MessageBox.Show("Success");
            //    }
            //    else
            //    {
            //        MessageBox.Show("Failed to insert.");
            //    }


            if (boosterManager.DeleteBooster("Shrouded Fable"))
            {
                MessageBox.Show("success");
            }
            else
            {
                MessageBox.Show("failed");
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

                if (artistManager.EditArtist(3, "Nothgin", ""))
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

    }
}
