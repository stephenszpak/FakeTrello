using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.DAL
{
    public interface IListRepository
    {
        #region read methods
        List GetList(int listId);
        List<List> GetListsFromBoard(int boardId);
        #endregion

        #region create methods
        void AddList(string name, Board board);
        void AddList(string name, int boardId);
        #endregion

        #region delete methods
        bool RemoveList(int listId);
        #endregion
    }
}
