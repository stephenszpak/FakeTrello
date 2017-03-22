using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FakeTrello.Models;
using System.Data.Common;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Diagnostics;

namespace FakeTrello.DAL
{
    public class ListRepository : IListRepository
    {
        //Create List
        #region Create List Method(s)
        public void AddList(string name, int boardId)
        {
            throw new NotImplementedException();
        }

        public void AddList(string name, Board board)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Get List
        #region Get List Method(s)
        public List GetList(int listId)
        {
            throw new NotImplementedException();
        }

        public List<List> GetListsFromBoard(int boardId)
        {
            throw new NotImplementedException();
        }
        #endregion

        //Delete List
        #region Delete List Method(s)
        public bool RemoveList(int listId)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}