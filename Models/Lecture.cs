using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Lecture
    {
        public Lecture()
        {
        }

        public Lecture(string input)
        {
            Theme = input;
        }

        public Lecture(Dictionary<string, object> data)
        {
            Int32.TryParse(data["id_lectures"].ToString(), out _id);
            Theme = data["theme"].ToString();
        }

        public string Theme { get; set; }

        private int _id = -1;

        public bool exists()
        {
            return _id != -1;
        }

        public int ID
        {
            get => _id;
        }

        public static List<Lecture> getAll()
        {
            List<Lecture> list = new List<Lecture>();
            foreach (var data in App.db.execute("select * from lectures where true;"))
            {
                list.Add(new Lecture(data));
            }

            return list;
        }

        public static Lecture getById(int id)
        {
            var data = App.db.execute("select * from lectures where id_lectures=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id}
                });
            if (data.Count == 0)
            {
                return new Lecture();
            }

            return new Lecture(data[0]);
        }

        public void delete()
        {
            App.db.execute("delete from lectures where id_lectures=@id;",
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
                    "UPDATE lectures SET theme = @_theme where id_lectures=@id",
                    new Dictionary<string, object>()
                    {
                        {"id", _id},
                        {"_theme", Theme},
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO lectures(theme) VALUES (@_theme) RETURNING id_lectures;",
                new Dictionary<string, object>()
                {
                    {"_theme", Theme},
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_lectures"].ToString(), out this._id);
            }
        }
    }
}