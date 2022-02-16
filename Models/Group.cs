using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Group
    {
        public Group()
        {
        }

        public Group(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_groups"].ToString(), out _id);
            name = data["group_name"].ToString();
        }

        public string name { get; set; }

        private int _id;

        public int ID
        {
            get => _id;
        }
        public static List<Group> getAll()
        {
            List<Group> list = new List<Group>();
            foreach (var data in App.db.execute("select * from Groups where true;")) //TODO проверить все ли будет норм с запросом
            {
                list.Add(new Group(data));
            }

            return list;
        }

        public static Group getById(int id)
        {
            var data = App.db.execute("select * from Groups where ID_Groups=@id;",
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
    }
}