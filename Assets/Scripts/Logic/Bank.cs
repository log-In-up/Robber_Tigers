namespace Assets.Scripts.Logic
{
    public class Bank : Building
    {
        public override void OnVisitTiger()
        {
            if (!isActiveAndEnabled) return;

            base.OnVisitTiger();
        }
    }
}