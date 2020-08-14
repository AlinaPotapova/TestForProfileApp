using System;
using Profile;
using Xunit;

namespace DemoLibraryTests
{
    public class Class1
    {
        [Theory]
        [InlineData("7", "Не присутсвует буква  в названии отдела")]
        [InlineData("п", "Не присутсвует цифра в названии отдела")]
        [InlineData("Отдел", "Не присутсвует цифра в названии отдела")]
        [InlineData("Отдеddл", "Название отдела не может содержать буквы латинского алфавита")]
        public void ChangeDepartment_ThrowsException(string department, string expectedInvalidMessage)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = Record.Exception(() => profile.ChangeDepartment(department));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("Отдел-3", true)]
        [InlineData("ОТДЕЛ-3", true)]
        [InlineData("ОтДел-3", true)]
        [InlineData("отдел-3", true)]

        public void ChangeDepartment_Successful(string department, bool expectedResult)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = profile.ChangeDepartment(department);

            //Assert
            Assert.Equal(expectedResult, ex);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("Потапова А А", "Отдел-3", "директор")]
        public void CreateProfile_Successful(string fullName, string department, string position)
        {
            //Arrange
            string expectedfullName = fullName.ToLower();
            string expecteddepartment = department.ToLower();
            string expectedposition = position.ToLower();

            //Act
            var actual = new ProfileStaffMember(fullName, department, position);

            //Assert
            Assert.Equal(expectedfullName, actual.FullName);
            Assert.Equal(expecteddepartment, actual.Department);
            Assert.Equal(expectedposition, actual.Pos);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("", "Отдел-6", "директор", "ФИО не может быть пустой строкой")]
        [InlineData("9", "Отдел-3", "директор", "ФИО не должно содержать цифры")]
        [InlineData("Потапова А.А", "Отдел", "директор", "Не присутсвует цифра в названии отдела")]
        [InlineData("Потапова А.А", "3", "директор", "Не присутсвует буква  в названии отдела")]
        [InlineData("Потапова А.А", "Отдел-3", "директо", "Данной должности не существует")]
        [InlineData("Потапова А.А", "Отдел-3", "", "Должность сотрудника не может быть пустой строкой")]
        [InlineData("Потапова А.А", "Отдел-3", "повар", "Данной должности не существует")]
        [InlineData("Потапsfdова А.А", "Отдел-3", "директор", "ФИО не должно содержать буквы латинского алфавита")]
        [InlineData("Потапова А.А", "Отдffел-3", "директор", "Название отдела не может содержать буквы латинского алфавита")]
        public void CreateProfile_ThrowsException(string fullName, string department, string position, string expectedInvalidMessage)
        {
            //Act
            var ex = Record.Exception(() => new ProfileStaffMember(fullName, department, position));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("РазНоРабочий", true)]
        [InlineData("директор", true)]
        [InlineData("Инженер", true)]
        [InlineData("РУКОВОДИТЕЛЬ", true)]
        public void ChangePosition_Successful(string position, bool expectedResult)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = profile.ChangePosition(position);

            //Assert
            Assert.Equal(expectedResult, ex);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("РазНоРабочи", "Данной должности не существует")]
        [InlineData("повар", "Данной должности не существует")]
        [InlineData("", "Должность сотрудника не может быть пустой строкой")]
        [InlineData("РУКОВОДТЕЛЬ", "Данной должности не существует")]
        public void ChangePosition_ThrowsException(string position, string expectedInvalidMessage)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = Record.Exception(() => profile.ChangePosition(position));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]

        [InlineData("", "РУКОВОДИТЕЛЬ", "Должность сотрудника не может быть пустой строкой")]
        [InlineData("повар", "", "Данной должности не существует")]
        [InlineData("РУКОВОДИТЕЛ", "РУКОВОДИТЕЛЬ", "Данной должности не существует")]
        [InlineData("ДИРЕКТОР", "РУКОВОДИТЕЛЬ", "Введена самая высокая должность")]

        public void UpdatePosition_ThrowsException(string previousPosition, string newPosition, string expectedInvalidMessage)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = Record.Exception(() => profile.PositionUpgrade(previousPosition, newPosition));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        // [InlineData("", "Директор", true)]
        [InlineData("инженер", "Директор", true)]
        [InlineData("руковоДитель", "Директор", true)]

        public void UpdatePosition_Successful(string previousPosition, string newPosition, bool expectedResult)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = profile.PositionUpgrade(previousPosition, newPosition);

            //Assert
            Assert.Equal(expectedResult, ex);

        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("инженер", true)]
        [InlineData("директор", true)]
        [InlineData("руководитель", true)]
        [InlineData("руководиТель", true)]

        public void PositionDemotion_Successful(string previousPosition, bool expectedResult)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = profile.PositionDemotion(previousPosition);

            //Assert
            Assert.Equal(expectedResult, ex);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("", "Должность сотрудника не может быть пустой строкой")]
        [InlineData("повар", "Данной должности не существует")]
        [InlineData("руководител", "Данной должности не существует")]
        [InlineData("разнорабочий", "Данная должность и так самая низкая в компании")]

        public void PositionDemotion_ThrowsException(string previousPosition, string expectedInvalidMessage)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember(null);

            //Act
            var ex = Record.Exception(() => profile.PositionDemotion(previousPosition));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("Потапова А.А5", "Отдел-4", "ФИО не должно содержать цифры")]
        [InlineData("Потапова А.А", "Отдел", "Не присутсвует цифра в названии отдела")]
        [InlineData("Потапова А.А", "4", "Не присутсвует буква  в названии отдела")]
        [InlineData("", "Отдел-4", "ФИО не может быть пустой строкой")]
        [InlineData("Потапов/а А.А", "Отдел-4", "Сотрудника с таким ФИО не существует")]
        [InlineData("Потапова А.А", "Отдел-123", "Такого отдела не существует в базе")]
        [InlineData("Потfgdfова А.А", "Отдел-123", "ФИО не должно содержать буквы латинского алфавита")]
        [InlineData("Потапова А.А", "Отдffел-123", "Название отдела не может содержать буквы латинского алфавита")]
        public void SearchDepartment_ThrowsException(string _fullName, string _department, string expectedInvalidMessage)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember("Потапова А.О", "Отдел-4", "директор");
            profile.ChangeDepartment("Отдел-3");
            profile.ChangeDepartment("Отдел-9");
            profile.ChangeDepartment("Отдел-5");

            //Act
            var ex = Record.Exception(() => profile.SearchDepartment(_fullName, _department));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("ПотапОва А.А", "Отдел-4", true)]
        [InlineData("Потапова А.А", "ОтДел-4", true)]
        [InlineData("Потапова А.А", "Отдел-4", true)]
        public void SearchDepartment_Successful(string _fullName, string _department, bool expectedResult)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember("Потапова А.А", "Отдел-4", "директор");
            profile.ChangeDepartment("Отдел-3");
            profile.ChangeDepartment("Отдел-9");
            profile.ChangeDepartment("Отдел-5");

            //Act
            var ex = profile.SearchDepartment(_fullName, _department);

            //Assert
            Assert.Equal(expectedResult, ex);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("ПотапОва А.А", "директор", true)]
        [InlineData("ПотапОва А.А", "диРектор", true)]
        [InlineData("Потапова А.А", "директор", true)]
        [InlineData("ПотапОва а.А", "диреКтор", true)]

        public void SearchPosition_Successful(string _fullName, string _position, bool expectedResult)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember("Потапова А.А", "Отдел-4", "директор");
            profile.ChangePosition("инженер");
            profile.ChangePosition("разнорабочий");
            profile.ChangePosition("руководитель");

            //Act
            var ex = profile.SearchPosition(_fullName, _position);

            //Assert
            Assert.Equal(expectedResult, ex);
        }

        ///////////////////////////////////////////////////////////////////////////////////////////

        [Theory]
        [InlineData("ПотапОв54а А.А", "директор", "ФИО не должно содержать цифры")]
        [InlineData("", "диРектор", "ФИО не может быть пустой строкой")]
        [InlineData("Потапова А.А", "", "Должность сотрудника не может быть пустой строкой")]
        [InlineData("ПотапОва а.А", "повар", "Данной должности не существует")]
        [InlineData("ПотапОв а.А", "директор", "Сотрудника с таким ФИО не существует")]
        [InlineData("ПотапавfsdпОв а.А", "директор", "ФИО не должно содержать буквы латинского алфавита")]

        public void SearchPosition_ThrowsException(string _fullName, string _position, string expectedInvalidMessage)
        {
            //Arrange
            ProfileStaffMember profile = new ProfileStaffMember("Потапова А.А", "Отдел-4", "директор");
            profile.ChangePosition("инженер");
            profile.ChangePosition("разнорабочий");
            profile.ChangePosition("руководитель");

            //Act
            var ex = Record.Exception(() => profile.SearchPosition(_fullName, _position));

            //Assert
            if (ex is ArgumentException argEx)
            {
                Assert.Equal(argEx.Message, expectedInvalidMessage);
            }
        }
    }

}


