namespace Assets.Scripts.Logic
{
    public class ButcheryShop : Building
    {
        public override void OnVisitTiger()
        {
            if (!isActiveAndEnabled) return;

            base.OnVisitTiger();
        }
    }
}