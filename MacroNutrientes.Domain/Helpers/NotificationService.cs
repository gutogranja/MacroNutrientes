using System.Collections.Generic;
using System.Linq;
using Dapper.Contrib.Extensions;
using MacroNutrientes.Domain.Interfaces.Notifications;

namespace MacroNutrientes.Domain.Helpers
{
    public class NotificationService : INotificationService
    {
        private List<Notification> _notificacoes { get; set; } = new List<Notification>();

        [Write(false)]
        public IReadOnlyCollection<Notification> Notificacoes { get { return _notificacoes; } }


        [Write(false)]
        public bool Validar { get { return !_notificacoes.Any(); } }

        public void AdicionarNotificacao(string key, string msg)
        {
            _notificacoes.Add(new Notification(key, msg));
        }

        public void AdicionarNotificacao(IReadOnlyCollection<Notification> listaNotificacoes)
        {
            _notificacoes.AddRange(listaNotificacoes);
        }

        public void LimparNotificacoes()    
        {
            _notificacoes = new List<Notification>();
        }
    }
}
