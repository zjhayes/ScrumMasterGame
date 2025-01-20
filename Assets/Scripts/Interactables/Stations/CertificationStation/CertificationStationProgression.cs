using UnityEngine;

[RequireComponent (typeof(CertificationStation))]
public class CertificationStationProgression : StationProgression
{
    private CertificationStation certificationStation;
    private ICharacterController student;
    private float lessonProgress;

    protected override void Awake()
    {
        certificationStation = GetComponent<CertificationStation>();

        certificationStation.OnFirstOccupant += ShowStudentProgress;
        certificationStation.OnUnoccupied += HideStudentProgress;

        progressBar.Maximum = CharacterStats.MAXIMUM;
        HideProgressBar();
    }

    private void ShowStudentProgress(ICharacterController student)
    {
        this.student = student;
        student.CertificationProgress.Progress.OnStatUpdated += UpdateFill;
        ShowProgressBar();
    }

    private void HideStudentProgress(ICharacterController student)
    {
        HideProgressBar();
        this.student.CertificationProgress.Progress.OnStatUpdated -= UpdateFill;
        this.student = null;
    }

    private void UpdateFill(CharacterStat stat)
    {
        if (certificationStation.Lesson == CharacterStat.FRONTEND)
        {
            progressBar.CurrentFill = student.CertificationProgress.Progress.Frontend;
        }
        else if (certificationStation.Lesson == CharacterStat.BACKEND)
        {
            progressBar.CurrentFill = student.CertificationProgress.Progress.Backend;
        }
        else if (certificationStation.Lesson == CharacterStat.PROBLEM_SOLVING)
        {
            progressBar.CurrentFill = student.CertificationProgress.Progress.ProblemSolving;
        }
        else if (certificationStation.Lesson == CharacterStat.TIME_MANAGEMENT)
        {
            progressBar.CurrentFill = student.CertificationProgress.Progress.TimeManagement;
        }
    }
}
