using System;
using System.Collections.Generic;
using System.Text;

namespace flight_management_system
{
    interface IBasicDb <T> where T : IPoco

    {
        void Add(T item);
        T Get(int id);
        List<T> GatAll();
        void Remove(T item);
        void Update(T item);

    }
}
