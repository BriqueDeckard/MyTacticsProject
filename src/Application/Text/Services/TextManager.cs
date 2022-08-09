namespace Assets.Scripts.src.Text.Services
{
    using Common.Contracts;
    using UnityEngine.UI;

    public class TextManager : Singleton<TextManager>
    {
        public Text InfoText1;

        // Start is called before the first frame update
        private void Start()
        {
            SetInfoTextText(InfoText.INFO_TEXT_1, "Hello");
        }

        public void SetInfoTextText(InfoText infoText, string text)
        {
            switch (infoText)
            {
                case InfoText.INFO_TEXT_1:
                    InfoText1.text = text;
                    break;

                default:
                    InfoText1.text = "";
                    break;
            }
        }
    }
}