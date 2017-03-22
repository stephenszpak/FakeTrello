using FakeTrello.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakeTrello.Controllers.Contracts
{
    public interface IBoardManager
    {
        void AddBoard(string name, ApplicationUser owner);
        bool RemoveBoard(int boardId);
        void EditBoardName(int boardId, string newname);
    }
}
