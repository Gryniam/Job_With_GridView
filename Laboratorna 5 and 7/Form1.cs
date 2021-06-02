using System;
using System.ComponentModel;
using System.IO;
using System.Windows.Forms;

namespace Laboratorna_5_and_7
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        //ОБ'ЄКТ КЛАСУ(БЕЗ ЦЬОГО НЕ ПРАЦЮЄ)
        DecanGroup decan = new DecanGroup("Grupa", 0);
        StudentsGroup studgrup = new StudentsGroup("Group", 0);

        private void Form1_Load(object sender, EventArgs e)
        {
            studgrup.Notify += SendWarningMail;
            Student one = new Student("Павлюк Михайло", 20, 65, 4);
            Student two = new Student("Лучинський Ярослав", 17, 80, 3);
            Student three = new Student("Ровінський Юрій", 18, 74, 5);
            studgrup += one;
            studgrup += two;
            studgrup += three;
            ZapTabl(studgrup);
            Decanat onee = new Decanat("Пилипів Володимир Михайлович", "pylypiv.v@gmail.com");
            Decanat twoo = new Decanat("Соломко Андрій Васильович", "ansolvas@gmail.com");
            Decanat threee = new Decanat("Горєлов Віталій Олевтинович", "vitaliy.goryelov@pnu.edu.ua");
            Decanat fourr = new Decanat("Боднаренко Ірина Ігорівна", "dekanat_mif@pu.if.ua");
            decan += onee;
            decan += twoo;
            decan += threee;
            decan += fourr;
            ZapTabl2(decan);

        }

        //ЗАПОВНЕННЯ ТАБЛИЦІ СТУДЕНТІВ(ФУНКЦІЯ)
        private void ZapTabl(StudentsGroup grup)
        {
            BindingList<Student> dani = new BindingList<Student>();
            studentsGrid.DataSource = dani;
            for (int i = 0; i < grup.students.Length; i++)
            {
                if (MarkFilterBox.Value < grup.students[i].Mark)
                {
                    dani.Add(grup.students[i]);
                }
                else
                {

                }
            }
        }

        //ФІШКА З ТЕКСТОМ ПРИ СОРТУВАННІ(ФУНКЦІЯ)
        private void readtext()
        {
            int sht = 0;
            char[] read = SortBox.Text.ToCharArray();
            for (int i = 1; i <= read.Length; i++)
            {
                sht++;
            }
            if (sht >= 0 && sht <= 3)
            {
                SortBox.Text = "Age";
            }
            else if (sht > 3 && sht < 5)
            {
                SortBox.Text = "Mark";
            }
            else if (sht >= 5)
            {
                SortBox.Text = "Weight";
            }
        }

        //СПОСОБИ СОРТУВАННЯ(ВАГА, ВІК, ОЦІНКА)(МЕТОДИ)
        private void SortWeight(StudentsGroup studgrup)
        {
            Array.Sort(studgrup.students, (ob1, ob2) => ob1.Weight.CompareTo(ob2.Weight));
            ZapTabl(studgrup);
        }
        private void SortAge(StudentsGroup studgrup)
        {
            Array.Sort(studgrup.students, (ob1, ob2) => ob1.Age.CompareTo(ob2.Age));
            ZapTabl(studgrup);
        }
        private void SortMark(StudentsGroup studgrup)
        {
            Array.Sort(studgrup.students, (ob1, ob2) => ob1.Mark.CompareTo(ob2.Mark));
            ZapTabl(studgrup);
        }

        //ДЕЛЕГАТ
        delegate void Sort(StudentsGroup studgrup);
        public void sorttype()
        {
            Sort sort1 = SortWeight;
            Sort sort2 = SortAge;
            Sort sort3 = SortMark;
            if (SortBox.Text == "age" || SortBox.Text == "Age")
            {
                sort2(studgrup);
            }
            else if (SortBox.Text == "weight" || SortBox.Text == "Weight")
            {
                sort1(studgrup);
            }
            else if (SortBox.Text == "mark" || SortBox.Text == "Mark")
            {
                sort3(studgrup);
            }
        }

        private void sortbutton(object sender, EventArgs e)
        {
            readtext();
            sorttype();
            SortBox.Clear();
        }

        private void Remove_student_button(object sender, EventArgs e)
        {
            string Delete = null;
            Delete = RemoveStudBox.Text;
            for (int i = 0; i < studgrup.array; i++)
            {
                if (studgrup.students[i].Name == Delete)
                {
                    studgrup -= studgrup.students[i];
                    break;
                }
            }
            ZapTabl(studgrup);
            RemoveStudBox.Clear();
        }

        private void Add_Student_Button(object sender, EventArgs e)
        {
            studgrup.Notify += SendWarningMail;
            if (StudNameBox.Text == "" || StudAgeBox.Text == "" || StudWeightBox.Text == "" || StudMarkBox.Text == "" ||
                StudNameBox.Text == " " || StudAgeBox.Text == " " || StudWeightBox.Text == " " || StudMarkBox.Text == " ")
            {
                MessageBox.Show("Будь ласка, заповність всі значення!");
            }
            else
            {
                string nName;
                int nAge;
                double nMass;
                int nmark;
                nName = StudNameBox.Text;
                nAge = Convert.ToInt32(StudAgeBox.Text);
                nMass = Convert.ToDouble(StudWeightBox.Text);
                nmark = Convert.ToInt32(StudMarkBox.Text);
                Student newStudent = new Student(nName, nAge, nMass, nmark);
                studgrup += newStudent;
                ZapTabl(studgrup);
            }
            StudNameBox.Clear();
            StudAgeBox.Clear();
            StudWeightBox.Clear();
            StudMarkBox.Clear();

        }

        private void Mark_Filter(object sender, EventArgs e)
        {
            ZapTabl(studgrup);
        }

        ///
        ///ДАЛІ КОД ДЕКАНАТУ
        ///

        //ЗАПОВНЕННЯ ТАБЛИЦІ ПРАЦІВНИКІВ ДЕКАНАТУ(ФУНКЦІЯ)
        private void ZapTabl2(DecanGroup group)
        {
            BindingList<Decanat> data = new BindingList<Decanat>();
            DecanatGrid.DataSource = data;
            for (int i = 0; i < group.human.Length; i++)
            {
                data.Add(group.human[i]);
            }

        }

        private void Add_human(object sender, EventArgs e)
        {
            if (DecanNameBox.Text == "" || DecanMailBox.Text == ""
                || DecanNameBox.Text == " " || DecanMailBox.Text == " ")
            {
                MessageBox.Show("Будь ласка, заповність всі значення!");
            }
            else
            {
                string nName;
                string nMail;
                nName = DecanNameBox.Text;
                nMail = DecanMailBox.Text;
                Decanat newHuman = new Decanat(nName, nMail);
                decan += newHuman;
                ZapTabl2(decan);
            }
            DecanNameBox.Clear();
            DecanMailBox.Clear();

        }

        private void Remove_human(object sender, EventArgs e)
        {
            string Delete = null;
            Delete = ReceiveDecanName.Text;
            for (int i = 0; i < decan.array; i++)
            {
                if (decan.human[i].Name == Delete)
                {
                    decan -= decan.human[i];
                    break;
                }
            }
            ZapTabl2(decan);
            ReceiveDecanName.Clear();
        }

        public static int count = 1;

        public void SendWarningMail(/*StudentsGroup grup, DecanGroup decan,*/ string ms)
        {
            for (int j = 0; j < decan.human.Length; j++, count++)
            {
                StreamWriter file = new StreamWriter(FileLocationBox.Text + decan.human[j].Name + " " + Convert.ToString(count) + " " + Convert.ToString(ms) + ".txt");
                file.Write("Відправник: PnuIF@gmail.com\n\n Отримувач: " + decan.human[j].Mail + " " +
                    "Студент, " + ms + " ,має заборгованість з предмету: Програмування.");
                file.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
    /// <summary>
    /// Два класи Student(з полями Name, Age, Weight, Mark) і StudentsGroup для роботи з Array 
    /// </summary>
    class Student
    {
        string name;
        int age;
        double mass;
        int mark;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public int Age
        {
            get { return age; }
            set { age = value; }
        }
        public double Weight
        {
            get { return mass; }
            set { mass = value; }
        }
        public int Mark
        {
            get { return mark; }
            set { mark = value; }
        }
        public Student(int mark)
        {
            Mark = mark;
        }
        public Student(string name, int age, double mass, int mark)
        {
            Name = name;
            Age = age;
            Weight = mass;
            Mark = mark;
        }
    }
    class StudentsGroup
    {
        public int array = 0; // значення довжини масиву
        public Student[] students = new Student[0];
        public string GroupName { get; set; }
        public delegate void Message(string ms);
        public event Message Notify;
        public StudentsGroup(string name, int lenght)
        {
            GroupName = name;
            array = lenght;
        }
        public StudentsGroup(StudentsGroup student, string n, int num = 0)
        {
            if (n == "+")//якщо додаю
            {
                GroupName = student.GroupName;
                array = student.array;
                students = new Student[student.array + 1];
                for (int i = 0; i < student.array; i++)
                {
                    students[i] = student.students[i];
                }
            }
            else// не додаю
            {
                bool temp = false;
                GroupName = student.GroupName;
                array = student.array - 2;
                students = new Student[student.array - 1];
                for (int i = 0; i < array + 1; i++)
                {
                    if (i == num)
                    {
                        students[i] = student.students[i + 1];
                        temp = true;
                        continue;
                    }
                    if (temp == true)
                    {
                        students[i] = student.students[i + 1];
                        continue;
                    }
                    students[i] = student.students[i];
                }
            }
        }
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
        public static StudentsGroup operator +(StudentsGroup ob1, Student student)
        {
            StudentsGroup newStudent = new StudentsGroup(ob1, "+");
            newStudent.students[newStudent.array] = student;
            newStudent.array++;

            if (student.Mark < 3)
            {
                ob1.Notify?.Invoke(student.Name);
            }

            return newStudent;
        }
        public static StudentsGroup operator -(StudentsGroup ob1, Student student)
        {
            int number = 0;
            for (int i = 0; i < ob1.array; i++)
            {
                if (SayNameForDel(ob1.students, student, i))
                {
                    number = i;
                    break;
                }
            }
            StudentsGroup newStudent = new StudentsGroup(ob1, "-", number);
            newStudent.array++;
            return newStudent;
        }
        private static bool SayNameForDel(Student[] stud, Student std, int i)
        {
            return stud[i].Name == std.Name;
        }
        //////////////////////////////////////////////////////////////////////////////
        //////////////////////////////////////////////////////////////////////////////
    }
    /// <summary>
    /// Два класи Decanat і DecanGroup,
    /// які зроблені за таким самим принципом як класс Student і StudentsGroup
    /// </summary>
    class Decanat
    {
        string name;
        string mail;
        public string Name
        {
            get
            { return name; }
            set
            { name = value; }
        }
        public string Mail
        {
            get
            {
                return mail;
            }
            set
            {
                mail = value;
            }
        }
        public Decanat(string name, string mail)
        {
            Name = name;
            Mail = mail;
        }
    }
    class DecanGroup
    {
        public int array = 0;
        public Decanat[] human = new Decanat[0];
        public string GroupName { get; set; }


        public DecanGroup(string name, int lenght)
        {
            GroupName = name;
            array = lenght;
        }
        public DecanGroup(DecanGroup decc, string t, int num = 0)
        {
            if (t == "+")//якщо додаю
            {
                GroupName = decc.GroupName;
                array = decc.array;
                human = new Decanat[decc.array + 1];
                for (int i = 0; i < decc.array; i++)
                {
                    human[i] = decc.human[i];
                }
            }
            else// не додаю
            {
                bool temp = false;
                GroupName = decc.GroupName;
                array = decc.array - 2;
                human = new Decanat[decc.array - 1];
                for (int i = 0; i < array + 1; i++)
                {
                    if (i == num)
                    {
                        human[i] = decc.human[i + 1];
                        temp = true;
                        continue;
                    }
                    if (temp == true)
                    {
                        human[i] = decc.human[i + 1];
                        continue;
                    }
                    human[i] = decc.human[i];
                }
            }
        }
        public static DecanGroup operator +(DecanGroup ob1, Decanat decc)
        {
            DecanGroup newHuman = new DecanGroup(ob1, "+");
            newHuman.human[newHuman.array] = decc;
            newHuman.array++;

            return newHuman;
        }
        public static DecanGroup operator -(DecanGroup ob1, Decanat decc)
        {
            int number = 0;
            for (int i = 0; i < ob1.array; i++)
            {
                if (SayNameToDel(ob1.human, decc, i))
                {
                    number = i;
                    break;
                }
            }
            DecanGroup newHuman = new DecanGroup(ob1, "-", number);
            newHuman.array++;
            return newHuman;
        }
        private static bool SayNameToDel(Decanat[] dec, Decanat de, int i)
        {
            return dec[i].Name == de.Name;
        }
    }
}


