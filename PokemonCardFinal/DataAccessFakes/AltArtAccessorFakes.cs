using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessInterfaces;
using DataDomain;

namespace DataAccessFakes
{
    public class AltArtAccessorFakes : IAltArtAccessor
    {
        List<AlternateArt> _alternateArts;

        /// <summary>
        /// Fills the _alternateArts list with fake data
        /// </summary>
        public AltArtAccessorFakes()
        {
            _alternateArts = new List<AlternateArt>();
            _alternateArts.Add(new AlternateArt()
            {
                AlternateArtID = "Test Alternate Art 1",
                Description = "This is a description 1.",
                Active = true,
            });
            _alternateArts.Add(new AlternateArt()
            {
                AlternateArtID = "Test Alternate Art 2",
                Description = "This is a description 2.",
                Active = true,
            });
            _alternateArts.Add(new AlternateArt()
            {
                AlternateArtID = "Test Alternate Art 3",
                Description = "This is a description 3.",
                Active = true,
            });
            _alternateArts.Add(new AlternateArt()
            {
                AlternateArtID = "Test Alternate Art 4",
                Description = "This is a description 4.",
                Active = false,
            });
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public AlternateArt SelectAlternateArtByID(string alternateArtID)
        {
            AlternateArt result = null;

            foreach (AlternateArt alternateArt in _alternateArts)
            {
                if (alternateArt.AlternateArtID == alternateArtID)
                {
                    result = alternateArt;
                }
            }

            return result;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public List<AlternateArt> SelectAllAlternateArt()
        {
            List<AlternateArt> results = new List<AlternateArt>();
            results = _alternateArts;
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<AlternateArt> SelectActiveAlternateArts(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<AlternateArt> results = new PaginatedResult<AlternateArt>();

            IEnumerable<AlternateArt> activeArts = _alternateArts.Where(art => art.Active);

            results.TotalCount = activeArts.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalPages = (int)Math.Ceiling((double)activeArts.Count() / pageSize);


            results.Items = activeArts.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public PaginatedResult<AlternateArt> SelectDeactiveAlternateArts(int pageNumber = 1, int pageSize = 20)
        {
            PaginatedResult<AlternateArt> results = new PaginatedResult<AlternateArt>();

            IEnumerable<AlternateArt> deactiveArts = _alternateArts.Where(art => !art.Active);

            results.TotalCount = deactiveArts.Count();
            results.PageNumber = pageNumber;
            results.PageSize = pageSize;
            results.TotalPages = (int)Math.Ceiling((double)deactiveArts.Count() / pageSize);


            results.Items = deactiveArts.Skip((pageNumber - 1) * pageSize)
                                           .Take(pageSize)
                                           .ToList();
            return results;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public int InsertAlternateArt(AlternateArt alternateArt)
        {
            int count = 0;

            foreach (AlternateArt element in _alternateArts)
            {
                if (element.AlternateArtID == alternateArt.AlternateArtID)
                {
                    throw new Exception("Alternate Art ID is already used.");
                }
            }

            _alternateArts.Add(alternateArt);
            count++;

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public int UpdateAlternateArt(AlternateArt alternateArt)
        {
            int count = 0;
            int index = 0;
            AlternateArt updatedAlternateArt = null;

            for (int i = 0; i < _alternateArts.Count; i++)
            {
                if (_alternateArts[i].AlternateArtID == alternateArt.AlternateArtID)
                {
                    updatedAlternateArt = _alternateArts[i];
                    index = i;
                    break;
                }
            }

            if (updatedAlternateArt != null)
            {
                updatedAlternateArt.AlternateArtID = alternateArt.AlternateArtID;
                updatedAlternateArt.Description = alternateArt.Description;
                _alternateArts[index] = updatedAlternateArt;
                count++;
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public int DeleteAlternateArt(string alternateArtID)
        {
            int count = 0;
            AlternateArt deletedAlternateArt = null;

            foreach (AlternateArt element in _alternateArts)
            {
                if (element.AlternateArtID == alternateArtID)
                {
                    count++;
                    deletedAlternateArt = element;
                }
            }

            if (count == 1)
            {
                _alternateArts.Remove(deletedAlternateArt);
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public int DeactivateAlternateArt(string alternateArtID)
        {
            int count = 0;
            foreach (AlternateArt arts in _alternateArts)
            {
                if (arts.AlternateArtID == alternateArtID)
                {
                    arts.Active = false;
                    count = 1;
                    break;
                }
            }

            return count;
        }

        /// <summary>
        /// Implements from <see cref="IAltArtAccessor"/> used for testing
        /// </summary>
        public int ReactivateAlternateArt(string alternateArtID)
        {
            int count = 0;
            foreach (AlternateArt arts in _alternateArts)
            {
                if (arts.AlternateArtID == alternateArtID)
                {
                    arts.Active = true;
                    count = 1;
                    break;
                }
            }

            return count;
        }
    }
}
