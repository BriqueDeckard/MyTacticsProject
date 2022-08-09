namespace Assets.Scripts.src.Panel.Services
{
    using Common.Contracts;
    using UnityEngine;

    public class PanelManager : Singleton<PanelManager>
    {
        public GameObject Panel1;

        private void Start()
        {
            TogglePanel(Panels.PANEL_1, true);
        }

        public void TogglePanel(Panels panel, bool active)
        {
            switch (panel)
            {
                case Panels.PANEL_1:
                    if (Panel1 != null)
                    {
                        Panel1.SetActive(active);
                    }
                    break;

                default:
                    Panel1.SetActive(false);
                    break;
            }
        }
    }
}