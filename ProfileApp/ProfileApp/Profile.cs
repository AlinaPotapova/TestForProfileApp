using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;


namespace Profile
{
    public class ProfileStaffMember
    {
        private int salary;
        private string fullName;
        private string pos;
        private string department;

        public string FullName { get; set; }
        public string Pos { get; set; }
        public string Department { get; set; }

        readonly ArrayList position = new ArrayList(4) { "директор", "руководитель", "инженер", "разнорабочий" }; //position

        Dictionary<string, string> _hist = new Dictionary<string, string>(); //история карьерных изменений

        readonly Dictionary<string, int> _salary = new Dictionary<string, int>
        {
         {"директор",50000},
         {"руководитель",20000},
         {"инженер",10000},
         {"разнорабочий",5000 },

        };

        public string CheckDepartment(string _department)
        {
            Regex regex = new Regex(@"[a-z]{1,}");
            if (regex.IsMatch(_department))
                throw new ArgumentException("Название отдела не может содержать буквы латинского алфавита");

            Regex regex2 = new Regex(@"[а-я]|[a-z]{1,}");
            Regex regex3 = new Regex(@"\d{1,}");

            if (regex3.IsMatch(_department) == false)
                throw new ArgumentException("Не присутсвует цифра в названии отдела");

            if (regex2.IsMatch(_department) == false)
                throw new ArgumentException("Не присутсвует буква  в названии отдела");

            return _department;
        }
        public ProfileStaffMember(string _fullName, string _department, string _position) //инициализирующий конструктор
        {
            fullName = CheckName(_fullName.ToUpper());

            department = CheckDepartment(_department.ToLower());

            pos = CheckPosition(_position.ToLower());

            _hist.Add(fullName, "Full Name");
            _hist.Add(department, "Department");
            _hist.Add(pos, "Position");


        }
        public ProfileStaffMember(string _position)
        {

            pos = _position;
        }
        public bool ChangeDepartment(string _newdepartment)//смена отдела
        {

            department = CheckDepartment(_newdepartment.ToLower());
            _hist.Add(department, "Department");
            return true;
        }
        public string CheckPosition(string _position)
        {
            foreach (object o in position)
            {
                if (_position == "")
                {
                    throw new ArgumentException("Должность сотрудника не может быть пустой строкой");
                }

                if (_position.ToLower() == o.ToString())
                {
                    pos = _position; ;
                    return _position;
                }
            }
            throw new ArgumentException("Данной должности не существует");

        }
        public string CheckName(string _fullName)
        {
            Regex regex2 = new Regex(@"[A-Z]{1,}");
            if (regex2.IsMatch(_fullName))
                throw new ArgumentException("ФИО не должно содержать буквы латинского алфавита");

            Regex regex = new Regex(@"(\d)");
            if (regex.IsMatch(_fullName))
                throw new ArgumentException("ФИО не должно содержать цифры");

            if (_fullName == "")
                throw new ArgumentException("ФИО не может быть пустой строкой");

            return _fullName;
        }
        public bool ChangePosition(string _newposition)//смена должности
        {
            pos = CheckPosition(_newposition.ToLower());
            _hist.Add(pos, "Position");
            return true;

        }
        public bool PositionUpgrade(string _previousPosition, string _newposition)//повышение по должности
        {
            if (_previousPosition.ToLower() == "директор")
            {
                throw new ArgumentException("Введена самая высокая должность");
            }

            CheckPosition(_previousPosition.ToLower());

            int index_position = position.IndexOf(_previousPosition.ToLower());
            string up_position = position[index_position - 1].ToString();
            _hist.Add(up_position, "Position");
            pos = up_position;
            return true;


        }
        public bool PositionDemotion(string _newposition)// понижение в должности
        {
            CheckPosition(_newposition);
            if (_newposition == "разнорабочий")
            {
                throw new ArgumentException("Данная должность и так самая низкая в компании");
            }
            int index_position = position.IndexOf(_newposition);
            string dem_position = position[index_position + 1].ToString();
            _hist.Add(dem_position, "Position");
            return true;
        }
        public void PositionCompare(ProfileStaffMember profile1, ProfileStaffMember profile2)//сравнение сотрудников по должности
        {

            if (pos.IndexOf(profile1.pos) > pos.IndexOf(profile2.pos))
                Console.WriteLine("Сотрудник {0} с позицией {1} находится выше по карьерной лестнице чем сотрудник {2}  с позицией {3}", profile1.fullName.ToUpper(), profile1.pos, profile2.fullName.ToUpper(), profile2.pos);

            if (pos.IndexOf(profile1.pos) == pos.IndexOf(profile2.pos))
                Console.WriteLine("Сотрудник {0} с позицией {1} занимает такую же должность как и  сотрудник {2} ", profile1.fullName.ToUpper(), profile1.pos, profile2.fullName.ToUpper());

            if (pos.IndexOf(profile1.pos) < pos.IndexOf(profile2.pos))
                Console.WriteLine("Сотрудник {0} с позицией {1} находится ниже по карьерной лестнице чем сотрудник {2}  с позицией {3}", profile1.fullName.ToUpper(), profile1.pos, profile2.fullName.ToUpper(), profile2.pos);


        }
        public void SalaryReceive(string _fullName, string _position)//получение заработной платы
        {

            if (_salary.ContainsKey(_position))
                salary = _salary[_position];
            else
                throw new ArgumentException("Не правильно введена должность сотрудника");
        }
        public bool SearchPosition(string _fullName, string _position)//поиск по должности
        {
            string name = CheckName(_fullName.ToUpper());
            string p = CheckPosition(_position.ToLower());

            if (_hist.ContainsKey(p))
            {
                Console.WriteLine("Должность-{0}", p);

            }

            if (_hist.ContainsKey(name))

            {
                Console.WriteLine("Должность-{0}", name);
            }

            else throw new ArgumentException("Сотрудника с таким ФИО не существует");
            return true;
        }
        public bool SearchDepartment(string _fullName, string _department)//поиск по отделу
        {
            string name = CheckName(_fullName.ToUpper());
            string dep = CheckDepartment(_department.ToLower());

            if (_hist.ContainsKey(dep))

                Console.WriteLine("Отдел-{0}", dep);

            else

                throw new ArgumentException("Такого отдела не существует в базе");

            if (_hist.ContainsKey(name))

                Console.WriteLine("ФИО-{0}", name);

            else
                throw new ArgumentException("Сотрудника с таким ФИО не существует");
            return true;

        }


    }
}






