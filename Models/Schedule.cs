using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class Schedule
    {
        public string Day { get; set; }
        public int Num { get=> _num; }
        private int _num;

        public Lecture Subject
        {
            get => Lecture.getById(_subject);
        }

        public Auditory Auditory
        {
            get => Auditory.GetById(_auditory);
        }

        public Group Group
        {
            get => Group.getById(_group);
        }

        public int ID
        {
            get => _id;
        }

        private int _subject;
        private int _auditory;
        private int _group;
        private int _id = -1;

        public Schedule()
        {
        }

        public Schedule(string input_day, Lecture input_subject, Auditory input_auditory, int input_num,
            Group input_group)
        {
            this.Day = input_day;
            this._num = input_num;
            //keys
            this._subject = input_subject.ID;
            this._auditory = input_auditory.ID;
            this._group = input_group.ID;
        }

        public Schedule(Dictionary<string, object> data)
        {
            this.Day = data["day"].ToString();
            Int32.TryParse(data["lecture_num"].ToString(), out this._num);
            Int32.TryParse(data["subject_fk"].ToString(), out this._subject);
            Int32.TryParse(data["auditorys_fk"].ToString(), out this._auditory);
            Int32.TryParse(data["group_fk"].ToString(), out this._group);
            Int32.TryParse(data["id_schelude"].ToString(), out this._id);
        }

        public static Schedule GetById(int id)
        {
            var data = App.db.execute("select * from schedule where id_schedule=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id}
                });
            if (data.Count == 0)
            {
                return new Schedule();
            }

            return new Schedule(data[0]);
        }
        public static List<Schedule> GetByGroupId(int id)
        {
            var data = App.db.execute("select * from schedule where group_fk=@id;",
                new Dictionary<string, object>()
                {
                    {"id", id}
                });
            if (data.Count == 0)
            {
                return new List<Schedule>{};
            }

            List<Schedule> output = new List<Schedule>();
            for (int i = 0; i < data.Count; i++)
            {
                output.Add(new Schedule(data[i]));
            }

            return output;
        }

        public static List<Schedule> getAll()
        {
            List<Schedule> list = new List<Schedule>();
            foreach (var data in App.db.execute("select * from schedule where true;"))
            {
                list.Add(new Schedule(data));
            }

            return list;
        }

        public bool exists()
        {
            return _id != -1;
        }

        public void Delete()
        {
            App.db.execute("delete from schedule where id_schedule=@id;",
                new Dictionary<string, object>()
                {
                    {"id", ID}
                });
            _id = -1;
        }

        public void save()
        {
            if (this.exists())
            {
                App.db.execute(
                    "UPDATE schedule SET day=@_day, subject_fk=@subj, auditories_fk=@audit, lecture_num=@number, group_fk=@group WHERE id_schedule=@id;",
                    new Dictionary<string, object>()
                    {
                        {"_day", Day},
                        {"subj", _subject},
                        {"audit", _auditory},
                        {"number", Num},
                        {"group", _group},
                        {"id", ID}
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO schedule(day, subject_fk, auditories_fk, lecture_num,group_fk) VALUES (@_day, @subj, @audit, @number,@group) RETURNING id_schelude;",
                new Dictionary<string, object>()
                {
                    {"_day", Day},
                    {"subj", _subject},
                    {"audit", _auditory},
                    {"number", Num},
                    {"group", _group}
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_schelude"].ToString(), out this._id);
            }
        }
    }
}