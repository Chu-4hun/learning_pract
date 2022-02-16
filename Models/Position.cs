using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Position
    {
        public Position()
        {
        }

        public Position(Dictionary<string, object> data)
        {
            name = data["pos_name"].ToString();
            Int32.TryParse(data["id_positions"].ToString(), out _id);
        }

        public string name { get; set; }

        private int _id;

        public int ID
        {
            get => _id;
        }
        public static List<Position> getAll()
        {
            List<Position> list = new List<Position>();
            foreach (var data in App.db.execute("select * from Positions where true;"))
            {
                list.Add(new Position(data));
            }

            return list;
        }

        public static Position getById(int id)
        {
            var data = App.db.execute("select * from Positions where ID_Positions=@id;",
                new Dictionary<string, object>()
                {
                    { "id", id }
                });
            if (data.Count == 0)
            {
                return new Position();
            }

            return new Position(data[0]);
        }
    }
}