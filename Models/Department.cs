using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Department
    {
        public Department()
        {
        }
        public Department(string dep_name)
        {
            name = dep_name;
        }

        public Department(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_department"].ToString(), out _id);
            name = data["dep_number"].ToString();
        }

        public string name { get; set; }

        private int _id = -1;

        public int ID
        {
            get => _id;
        }
        public bool exists()
        {
            return _id != -1;
        }
        public static List<Department> getAll()
        {
            List<Department> list = new List<Department>();
            foreach (var data in App.db.execute("select * from department where true;"))
            {
                list.Add(new Department(data));
            }

            return list;
        }

        public static Department getById(int id)
        {
            var data = App.db.execute("select * from Department where ID_Department=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id}
                });
            if (data.Count == 0)
            {
                return new Department();
            }

            return new Department(data[0]);
        }
        public void delete()
        {
            App.db.execute("delete from department where id_department=@id;",
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
                    "UPDATE department SET dep_number=@_name where id_department=@id",
                    new Dictionary<string, object>()
                    {
                        {"id", _id},
                        {"_name", name},
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO department(dep_number) VALUES (@_name) RETURNING id_department;",
                new Dictionary<string, object>()
                {
                    {"_name", name},
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_department"].ToString(), out this._id);
            }
        }
    }
}