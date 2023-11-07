using System.Collections.Generic;
using UnityEngine.UI;

namespace Main.Scripts.Infrastructure.Services.ButtonContainer
{
    public interface IButtonContainerService : IService
    {
        List<Button> Buttons { get; }
        
        void AddButton(Button button);
        
        void AddButtons(List<Button> buttons);

        void RemoveButton(Button button);

        void EnableAllButtons();

        void DisableAllButtons();
    }
}