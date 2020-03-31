using Dapper.Contrib.Extensions;
using MacroNutrientes.Domain.Helpers;
using System.Collections.Generic;

namespace MacroNutrientes.Domain.Interfaces.Notifications
{
    public interface INotificationService
    {
        [Write(false)]
        IReadOnlyCollection<Notification> Notificacoes { get; }

        [Write(false)]
        bool Validar { get; }
        void AdicionarNotificacao(IReadOnlyCollection<Notification> notificacoes);
        void LimparNotificacoes();
    }
}
