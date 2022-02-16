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


        public Auditory(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_auditory"].ToString(), out _id);
            Int32.TryParse(data["auditory_num"].ToString(), out num);
            Int32.TryParse(data["capacity"].ToString(), out capacity);
        }

        public int num;
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

        public void delete(int _inputid)
        {
            App.db.execute("delete from auditorys where id_auditory=@_inputid;");
            _id = -1;
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
                    "UPDATE auditorys SET id_Auditory=@id, Auditory_num=@name, capacity=@capacity WHERE id_auditory=@id;",
                    new Dictionary<string, object>()
                    {
                        {"num", num},
                        {"capacity", capacity},
                        {"id", ID},
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO Users(id_Auditory, Auditory_num, capacity) VALUES (@id, @name, @capacity, @login) RETURNING id_auditory;",
                new Dictionary<string, object>()
                {
                    {"num", num},
                    {"capacity", capacity},
                    {"id", ID},
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_auditory"].ToString(), out this._id);
            }
        }
    }
}