using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string surname, string firstName, string patronymic, Position position)
        {
            this.surname = surname;
            this.firstName = firstName;
            this.patronymic = patronymic;
            this.login = "-";
            this.password = "-";
            this._idPosition = position.ID;
        }

        public User(Dictionary<string, object> data)
        {
            this.surname = data["surname"].ToString();
            this.firstName = data["name"].ToString();
            this.patronymic = data["patronymic"].ToString();
            this.login = data["login"].ToString();
            this.password = data["password"].ToString();
            Int32.TryParse(data["id_user"].ToString(), out this._id);
            var Position = App.db.execute("select * from positions_to_users where user_fk=@id;",
                new Dictionary<string, object>()
                {
                    { "id", ID }
                });
            Int32.TryParse( Position[0]["positions_fk"].ToString(), out this._idPosition);
        }

        public string surname { get; set; }

        public string firstName { get; set; }

        public string patronymic { get; set; }
        
        public string FIO
        {
            get => surname+" "+firstName+" "+patronymic;
        }

        public string login { get; set; }

        public string password { get; set; }
        

        private int _idPosition;

        public Position Position
        {
            get => Position.getById(_idPosition);
        }

        private int _id = -1;

        public int ID
        {
            get => _id;
        }

        public static User getById(int id)
        {
            var data = App.db.execute("select * from Users where ID_user=@id;",
                new Dictionary<string, object>()
                {
                    { "id", id }
                });
            if (data.Count == 0)
            {
                return new User();
            }

            return new User(data[0]);
        }

        public static User getByLoginPassword(string login, string password)
        {
            var data = App.db.execute("select * from Users where login=@login and password=@password;",
                new Dictionary<string, object>()
                {
                    { "login", login },
                    { "password", password },
                });
            if (data.Count == 0)
            {
                return new User();
            }

            return new User(data[0]);
        }
        
        public static List<User> getAll()
        {
            List<User> users = new List<User>();
            foreach (var data in App.db.execute("select * from Users where true;"))
            {
                users.Add(new User(data));
            }

            return users;
        }
        public bool exists()
        {
            return _id != -1;
        }

        public void delete()
        {
            App.db.execute("delete from Users where ID_user=@id;",
                new Dictionary<string, object>()
                {
                    { "id", ID }
                });
            _id = -1;
        }
        public void save()
        {
            // if (this.exists())
            // {
            //     App.db.execute(
            //         "UPDATE personal SET fam_personal=@surname, name_personal=@firstName, otch_personal=@patronymic, login=@login, password=@password WHERE id_personal=@id_personal;",
            //         new Dictionary<string, object>()
            //         {
            //             { "surname", surname },
            //             { "firstName", firstName },
            //             { "patronymic", patronymic },
            //             { "login", login },
            //             { "password", password },
            //             { "id", _id },
            //         });
            //     return;
            // }

            var data = App.db.execute(
                "INSERT INTO Users(surname, name, patronymic, login, password) VALUES (@surname, @firstName, @patronymic, @login, @password) RETURNING id_personal;",
                new Dictionary<string, object>()
                {
                    { "surname", surname },
                    { "firstName", firstName },
                    { "patronymic", patronymic },
                    { "login", login },
                    { "password", password },
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_personal"].ToString(), out this._id);
            }
        }
    }
}