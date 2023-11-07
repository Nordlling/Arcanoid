using System.Collections.Generic;
using UnityEngine.UI;

namespace Main.Scripts.Infrastructure.Services.ButtonContainer
{
    public class ButtonContainerService : IButtonContainerService
    {
        public List<Button> Buttons { get; } = new();

        public void AddButton(Button button)
        {
            Buttons.Add(button);
        }

        public void AddButtons(List<Button> buttons)
        {
            Buttons.AddRange(buttons);
        }

        public void RemoveButton(Button button)
        {
            Buttons.Remove(button);
        }

        public void EnableAllButtons()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].interactable = true;
            }
        }

        public void DisableAllButtons()
        {
            for (int i = 0; i < Buttons.Count; i++)
            {
                Buttons[i].interactable = false;
            }
        }
    }
}