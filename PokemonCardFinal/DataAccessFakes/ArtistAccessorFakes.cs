using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class ArtistAccessorFakes : IArtistAccessor
    {
        List<Artist> _artists;

        /// <summary>
        /// Fills the _artists list with fake data
        /// </summary>
        public ArtistAccessorFakes()
        {
            _artists = new List<Artist>();
            _artists.Add(new Artist()
            {
                ArtistID = 1,
                GivenName = "Test Given 1",
                Surname = "Test Surname 1",
                Active = true,

            });
            _artists.Add(new Artist()
            {
                ArtistID = 2,
                GivenName = "Test Given 2",
                Surname = "Test Surname 2",
                Active = true,
            });
            _artists.Add(new Artist()
            {
                ArtistID = 3,
                GivenName = "Test Given 3",
                Surname = "",
                Active = true,
            });
            _artists.Add(new Artist()
            {
                ArtistID = 4,
                GivenName = "Test Given 4",
                Surname = "Test Surname 4",
                Active = false,
            });
            _artists.Add(new Artist()
            {
                ArtistID = 5,
                GivenName = "Test Given 5",
                Surname = "Test Surname 5",
                Active = false,
            });
        }

        /// <summary>
        /// Implements from <see cref="IArtistAccessor"/> used for testing
        /// </summary>
        public Artist SelectArtistByArtistID(int artistID)
        {
            Artist resultArtist = null;

            foreach (Artist artist in _artists)
            {
                if (artist.ArtistID == artistID)
                {
                    resultArtist = artist;
                    break;
                }
            }

            if (resultArtist == null)
            {
                throw new ArgumentException("Artist ID could not be found.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public Artist SelectArtistByArtistName(string givenName, string surname)
        {

            Artist resultArtist = null;

            foreach (Artist artist in _artists)
            {
                if (artist.GivenName == givenName && artist.Surname == surname)
                {
                    resultArtist = artist;
                    break;
                }
            }

            if (resultArtist == null)
            {
                throw new ArgumentException("Artist ID could not be found.");
            }

            return resultArtist;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public List<Artist> SelectAllArtists()
        {
            List<Artist> results = null;
            results = _artists;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<Artist> SelectActiveArtists(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Artist> results = new PaginatedResult<Artist>();

            IEnumerable<Artist> activeArtists = _artists.Where(ability => ability.Active);

            results.TotalCount = activeArtists.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalPages = (int)Math.Ceiling((double)activeArtists.Count() / pageSize);


            results.Items = activeArtists.Skip((pageNumber - 1) * pageSize)
                                          .Take(pageSize)
                                          .ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<Artist> SelectDeactiveArtists(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<Artist> results = new PaginatedResult<Artist>();

            IEnumerable<Artist> deactiveArtists = _artists.Where(ability => !ability.Active);

            results.TotalCount = deactiveArtists.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalPages = (int)Math.Ceiling((double)deactiveArtists.Count() / pageSize);


            results.Items = deactiveArtists.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int InsertArtist(string givenName, string surname)
        {
            int result = 0;
            int count = 0;

            Artist newArtist = new Artist()
            {
                ArtistID = 4,
                GivenName = givenName,
                Surname = surname
            };

            foreach (Artist artist in _artists)
            {
                if (artist.GivenName == givenName && artist.Surname == surname)
                {
                    count = artist.ArtistID;
                    break;
                }
            }

            if (count == 0)
            {
                _artists.Add(newArtist);
                result = newArtist.ArtistID;
            }
            else
            {
                result = 0;
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int UpdateArtist(int artistID, string givenName, string surname)
        {
            int count = 0;
            Artist updatedArtist = null;

            foreach (Artist artist in _artists)
            {
                if (artist.ArtistID == artistID)
                {
                    updatedArtist = artist;
                    break;
                }
            }

            if (updatedArtist != null)
            {
                updatedArtist.GivenName = givenName;
                updatedArtist.Surname = surname;

                foreach (Artist artist in _artists)
                {
                    if (updatedArtist.GivenName == artist.GivenName && updatedArtist.Surname == artist.Surname)
                    {
                        count++;
                    }
                }
            }
            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int DeleteArtist(int artistID)
        {
            int count = 0;
            Artist deletedArtist = null;

            foreach (Artist artist in _artists)
            {
                if (artist.ArtistID == artistID)
                {
                    deletedArtist = artist;
                    break;
                }
            }

            if (deletedArtist != null)
            { 
                _artists.Remove(deletedArtist);
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int DeactivateArtist(int artistID)
        {
            int count = 0;

            foreach (Artist artist in _artists)
            {
                if (artist.ArtistID == artistID)
                {
                    artist.Active = true;
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IElementAccessor"/> used for testing
        /// </summary>
        public int ReactivateArtist(int artistID)
        {
            int count = 0;

            foreach (Artist artist in _artists)
            {
                if (artist.ArtistID == artistID)
                {
                    artist.Active = false;
                    count++;
                }
            }

            return count;
        }
    }
}
