using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.DAL
{
    public interface ICardRepository
    {
        #region Create Card Method(s)
        void AddCard(string name, List list, ApplicationUser owner);
        void AddCard(string name, int listId, string ownerId);
        #endregion

        #region Read Card Method(s)
        List<Card> GetCardsFromList(int listId);
        List<Card> GetCardsFromBoard(int boardId);
        Card GetCard(int cardId);
        List<ApplicationUser> GetCardAttendees(int cardId);
        #endregion

        #region Update Card Method(s)
        bool AttachUser(string userId, int cardId);
        bool MoveCard(int cardId, int oldListId, int newListId);
        bool CopyCard(int cardId, int newListId, string newOwnerId);
        #endregion

        #region Delete Card Method(s)
        bool RemoveCard(int cardId);
        #endregion

    }
}
