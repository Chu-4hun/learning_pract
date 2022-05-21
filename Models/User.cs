using System;
using System.Collections.Generic;

namespace learning_pract.Models
{
    public class User
    {
        public User()
        {
        }

        public User(string surname, string firstName, string patronymic, string login, string password, Position position)
        {
            this.surname = surname;
            this.firstName = firstName;
            this.patronymic = patronymic;
            this.login = login;
            this.password = password;
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
            Int32.TryParse( data["position_fk"].ToString(), out this._idPosition);
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
        

        public int _idPosition;

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
                    { "id", id}
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
            if (this.exists())
            {
                App.db.execute(
                    "UPDATE Users SET surname=@surname, name=@name, patronymic=@patronymic, login=@login, password=@password, position_fk=@position WHERE id_user=@id;",
                    new Dictionary<string, object>()
                    {
                        { "surname", surname },
                        { "name", firstName },
                        { "patronymic", patronymic },
                        { "login", login },
                        { "password", password },
                        {"position", _idPosition},
                        { "id", ID }
                    });
                return;
            }

            var data = App.db.execute(
                "INSERT INTO Users(surname, name, patronymic, login, password,position_fk) VALUES (@surname, @firstName, @patronymic, @login, @password,@position) RETURNING id_user;",
                new Dictionary<string, object>()
                {
                    { "surname", surname },
                    { "firstName", firstName },
                    { "patronymic", patronymic },
                    { "login", login },
                    { "password", password },
                    {"position", _idPosition}
                });
            if (data.Count > 0)
            {
                Int32.TryParse(data[0]["id_user"].ToString(), out this._id);
            }
        }
    }
}