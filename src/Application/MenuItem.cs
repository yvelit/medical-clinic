using System;

namespace Application
{
    //Classe que lida com os elementos do menu da interface do usuário
    internal class MenuItem
    {
        public MenuItem(string titulo, Action action)
        {
            Titulo = titulo??throw new ArgumentNullException(nameof(titulo));
            Action = action??throw new ArgumentNullException(nameof(action));
        }

        public string Titulo { get; private set; }
        public Action Action { get; private set; }
    }
}
