namespace PoliclinicTests;

public class PoliclinicTest : IClassFixture<PoliclinicTestFixture>
{
    private PoliclinicTestFixture _fixture;

    public PoliclinicTest(PoliclinicTestFixture fixture)
    {
        _fixture = fixture;
    }
    /// <summary>
    /// ������� ���������� � ���� ������, ���� ������ ������� �� ������ 10 ���
    /// </summary>
    [Fact]
    public void FirstRequest()
    {
        var doctorList = _fixture.CreateDefaultDoctors();
        var requestDoctorList = (from doctor in doctorList
                                 where doctor.WorkExperience >= 10
                                 select doctor.FIO).ToList();
        Assert.Equal(doctorList[1].FIO, requestDoctorList[0]);
    }
    /// <summary>
    /// ������� ���������� � ���� ���������, ���������� �� ����� � ���������� �����, ����������� �� ���
    /// </summary>
    [Fact]
    public void SecondRequest()
    {
        var receptionList = _fixture.CreateDefaultReceptions();
        var patientList = _fixture.CreateDefaultPatients();
        var doctorList = _fixture.CreateDefaultDoctors();
        var requestPatientList = (from patient in patientList
                                  join reception in receptionList on patient.Passport equals (reception.PassportPatient)
                                  join doctor in doctorList on reception.PassportDoctor equals (doctor.Passport)
                                  where doctor.FIO == "������ ���� ��������" && doctor.FIO.Count() > 1
                                  orderby patient.FIO
                                  select patient.FIO).ToList();
        Assert.Equal("����� ������� ����������", requestPatientList[0]);
        Assert.Equal("������ ���� ������������", requestPatientList[1]);
    }
    /// <summary>
    /// ������� ���������� � �������� �� ��������� ������ ���������
    /// </summary>
    [Fact]
    public void ThirdRequest()
    {
        var receptionList = _fixture.CreateDefaultReceptions();
        var requestHealthyPatientList = (from reception in receptionList
                                         where reception.Status == "������"
                                         select reception.PassportPatient).ToList();
        Assert.Equal(receptionList[1].PassportPatient, requestHealthyPatientList[0]);
        Assert.Equal(receptionList[2].PassportPatient, requestHealthyPatientList[1]);
    }
    /// <summary>
    /// ������� ���������� � ���������� ������� ��������� �� ������ �� ��������� �����
    /// </summary>
    [Fact]
    public void FourthRequest()
    {
        var receptionList = _fixture.CreateDefaultReceptions();
        var requestCountReceptionsInOneMonth = (from reception in receptionList
                                                where reception.DateAndTime > new DateTime(2023, 1, 31)
                                                && reception.DateAndTime < new DateTime(2023, 3, 1) && reception.PassportDoctor == 1234567890
                                                select reception.IdReception).Count();
        Assert.Equal(2, requestCountReceptionsInOneMonth);
    }
    /// <summary>
    /// ������� ���������� � ��� 5 �������� ���������������� ������������ ����� ���������
    /// </summary>
    [Fact]
    public void FifthRequest()
    {
        var receptionList = _fixture.CreateDefaultReceptions();
        var requestTopDiseases = (from reception in receptionList
                                  where reception.Conclution != ""
                                  orderby reception.Conclution.Count()
                                  select reception.Conclution).Take(5).ToList();
        Assert.Equal("������", requestTopDiseases[0]);
        Assert.Equal("�������", requestTopDiseases[1]);
    }
    /// <summary>
    /// ������� ���������� � ��������� ������ 30 ���, ������� �������� �� ����� � ���������� ������, ����������� �� ���� ��������
    /// </summary>
    [Fact]
    public void SixthRequest()
    {
        var receptionList = _fixture.CreateDefaultReceptions();
        var patientList = _fixture.CreateDefaultPatients();
        var requestOlderPatients = (from patient in patientList
                                    join reception in receptionList on patient.Passport equals (reception.PassportPatient)
                                    where DateTime.Today.Year - patient.BirthDate.Year > 30 && reception.Status.Count() > 1
                                    orderby patient.BirthDate
                                    select patient.FIO).ToList();
        Assert.Equal("����� ������� ����������", requestOlderPatients[0]);
    }
}