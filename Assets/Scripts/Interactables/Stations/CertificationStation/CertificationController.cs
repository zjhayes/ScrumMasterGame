using UnityEngine;

[RequireComponent(typeof(CertificationStation))]
public class CertificationController : GameBehaviour
{
    [SerializeField]
    private AnimationCurve learningCurve;
    [SerializeField]
    private float learnRate = 0.25f;

    private CertificationStation certificationStation;
    private ICharacterController student;
    private float lessonProgress;

    private void Awake()
    {
        certificationStation = GetComponent<CertificationStation>();

        certificationStation.OnFirstOccupant += Teach;
        certificationStation.OnUnoccupied += StopTeaching;
        this.enabled = false;
    }
    
    private void Update()
    {
        if (certificationStation.Lesson == CharacterStat.FRONTEND)
        {
            lessonProgress += CalculateProgress(student.Stats.Frontend);

            if (lessonProgress >= Numeric.ONE_HUNDRED_PERCENT)
            {
                student.CertificationProgress.Progress.Frontend += 1;
                lessonProgress = Numeric.ZERO;
            }
        }
        else if (certificationStation.Lesson == CharacterStat.BACKEND)
        {
            lessonProgress += learningCurve.Evaluate(student.Stats.Backend);

            if (lessonProgress >= Numeric.ONE_HUNDRED_PERCENT)
            {
                student.CertificationProgress.Progress.Backend += 1;
                lessonProgress = Numeric.ZERO;
            }
        }
        else if (certificationStation.Lesson == CharacterStat.PROBLEM_SOLVING)
        {
            lessonProgress += learningCurve.Evaluate(student.Stats.ProblemSolving);

            if (lessonProgress >= Numeric.ONE_HUNDRED_PERCENT)
            {
                student.CertificationProgress.Progress.ProblemSolving += 1;
                lessonProgress = Numeric.ZERO;
            }
        }
        else if (certificationStation.Lesson == CharacterStat.TIME_MANAGEMENT)
        {
            lessonProgress += learningCurve.Evaluate(student.Stats.TimeManagement);

            if (lessonProgress >= Numeric.ONE_HUNDRED_PERCENT)
            {
                student.CertificationProgress.Progress.TimeManagement += 1;
                lessonProgress = Numeric.ZERO;
            }
        }
    }

    private float CalculateProgress(int currentStat)
    {
        return learningCurve.Evaluate(currentStat) * learnRate;
    }

    private void Teach(ICharacterController student)
    {
        this.student = student;
        this.enabled = true;
    }

    private void StopTeaching(ICharacterController student)
    {
        this.enabled = false;
        this.student = null;
    }
}
