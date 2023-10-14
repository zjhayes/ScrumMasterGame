
public class CertificationStationProgression : StationProgression
{
    private CertificationStation certificationStation;

    protected override void Awake()
    {
        certificationStation = GetComponent<CertificationStation>();

        certificationStation.OnFirstOccupant += Teach;
        certificationStation.OnUnoccupied += StopTeaching;
        this.enabled = false;
        base.Awake();
    }

    private void Teach(ICharacterController student)
    {
        this.enabled = true;
        ShowProgressBar();
    }

    private void StopTeaching(ICharacterController student)
    {
        this.enabled = false;
        HideProgressBar();
    }
}
