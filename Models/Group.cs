using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Group
    {
        public Group()
        {
        }

        public Group(string input)
        {
            name = input;
        }

        public Group(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_groups"].ToString(), out _id);
            name = data["group_name"].ToString();
        }

        public string name { get; set; }

        private int _id = -1;

        public bool exists()
        {
            return _id != -1;
        }

        public int ID
        {
            get => _id;
        }

        public static List<Group> getAll()
        {
            List<Group> list = new List<Group>();
            foreach (var data in App.db.execute("select * from groups where true;"))
            {
                list.Add(new Group(data));
            }

            return list;
        }

        public static Group getById(int id)
        {
            var data = App.db.execute("select * from groups where ID_Groups=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id}
                });
            if (data.Count == 0)
            {
                return new Group();
            }

            return new Group(data[0]);
        }

        public void delete()
        {
            App.db.execute("delete from groups where id_groups=@id;",
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
                    "UPDATE groups SET group_name =@_name where id_groups=@id",
                    new Dictionary<string, object>()
                    {
                        {"id", _id},
                        {"_name", name},
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO groups(group_name) VALUES (@_name) RETURNING id_groups;",
                new Dictionary<string, object>()
                {
                    {"_name", name},
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_groups"].ToString(), out this._id);
            }
        }
    }
}