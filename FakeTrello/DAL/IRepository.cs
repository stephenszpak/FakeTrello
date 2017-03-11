using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.DAL
{
    public interface IRepository
    {
        // List of methods to help deliver features

        // Create
        void AddBoard(string name, ApplicationUser owner);
        void AddList(string name, Board board);
        void AddList(string name, int boardId);
        void AddCard(string name, List list, ApplicationUser owner);
        void AddCard(string name, int listId, string ApplicationUserId);

        // Read
        List<Card> GetCardsFromList(int listId);
        List<Card> GetCardsFromBoard(int boardId);
        Card GetCard(int cardId);
        List GetList(int listId);
        List<List> GetListsFromBoard(int boardId); // List of trello lists.
        List<Board> GetBoardsFromUser(string ApplicationUserId);
        Board GetBoard(int boardId);
        List<ApplicationUser> GetCardAttendees(int cardId);

        // Update
        bool AttachUser(string ApplicationUserId, int CardId); // true: successful, false: not successful
        bool MoveCard(int cardId, int oldListId, int newListId);
        bool CopyCard(int cardId, int newListId, string newOwnerId);

        // Delete
        bool RemoveBoard(int boardId); // Lets the front-end know this stuff has been deleted by using BOOL
        bool RemoveList(int listId);
        bool RemoveCard(int cardId);
    }
}
