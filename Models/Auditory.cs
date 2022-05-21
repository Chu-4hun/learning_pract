using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Auditory
    {
        public Auditory()
        {
        }

        public Auditory(int _AuditNum, int _capacity)
        {
            num = _AuditNum;
            capacity = _capacity;
        }
        public Auditory(String _AuditNum, String _capacity)
        {
            int vrem;
            Int32.TryParse(_AuditNum,out vrem);
            num = vrem;
            Int32.TryParse(_capacity,out capacity);
        }


        public Auditory(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_auditory"].ToString(), out _id);
            int vrem;
            Int32.TryParse(data["auditory_num"].ToString(), out vrem);
            num = vrem;
            Int32.TryParse(data["capacity"].ToString(), out capacity);
        }

        public int num { get; set; }
        public int capacity;

        private int _id = -1;

        public int ID
        {
            get => _id;
        }

        public bool exists()
        {
            return _id != -1;
        }
        
        public static List<Auditory> GetAll()
        {
            List<Auditory> list = new List<Auditory>();
            foreach (var data in
                     App.db.execute(
                         "select * from Auditorys where true;")) //TODO проверить все ли будет норм с запросом
            {
                list.Add(new Auditory(data));
            }

            return list;
        }

        public static Auditory GetById(int id)
        {
            var data = App.db.execute("select * from Auditorys where ID_Auditory=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id}
                });
            if (data.Count == 0)
            {
                return new Auditory();
            }

            return new Auditory(data[0]);
        }

        public void delete()
        {
            App.db.execute("delete from auditorys where id_auditory=@id;",
                new Dictionary<string, object>()
                {
                    {"id", ID}
                });
            _id = -1;
        }

        public void Save()
        {
            if (this.exists())
            {
                App.db.execute(
                    "UPDATE auditorys SET Auditory_num=@num, capacity=@capacity where id_Auditory=@id",
                    new Dictionary<string, object>()
                    {
                        {"id", _id},
                        {"num", num},
                        {"capacity", capacity},
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO auditorys(Auditory_num, capacity) VALUES (@num, @capacity) RETURNING id_auditory;",
                new Dictionary<string, object>()
                {
                    {"num", num},
                    {"capacity", capacity},
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_auditory"].ToString(), out this._id);
            }
        }
    }
}