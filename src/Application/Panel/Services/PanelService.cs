namespace Assets.Scripts.src.Application.Panel.Services
{
    using Common.Contracts;
    using UnityEngine;

    public class PanelService : Singleton<PanelService>
    {
        public GameObject Panel1;

        public bool Panel1IsActive = false;

        private void Start()
        {
            TogglePanel(Panels.PANEL_1, true);
        }

        private void Update()
        {
            Panel1IsActive = Panel1.activeInHierarchy;
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