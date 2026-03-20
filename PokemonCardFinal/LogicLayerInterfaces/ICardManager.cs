using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataDomain;

namespace LogicLayerInterfaces
{
    public interface ICardManager 
    {
        public CardVM GetCardVM(int cardID);

        public List<Card> GetAllCards();

        public int AddCard(Card card);

        public bool EditCard(Card card);

        public bool DeleteCard(int cardID);
    }
}
