using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Domain;
namespace Graph
{
    interface ADT_Graph
    {
        User GetRoot();
        User Clone();
        User Add(User e);
        User Remove(User e);
        List<User> GetConnection(User e);
        List<User> FindElementByName(String Name);
        User FindElementById(int Id);

    }
}
