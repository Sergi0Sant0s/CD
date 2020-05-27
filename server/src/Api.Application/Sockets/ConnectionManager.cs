using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;

namespace Api.Application.Sockets
{
  public class ConnectionManager
  {
    private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

    /// <summary>
    /// Obter um socket pelo seu id
    /// </summary>
    /// <param name="id">Id do socket a obter</param>
    /// <returns>Retorna o socket com o id escolhido</returns>
    public WebSocket GetSocketById(string id)
    {
      return _sockets.FirstOrDefault(p => p.Key == id).Value;
    }

    /// <summary>
    /// Dicionario com todos os sockets
    /// </summary>
    /// <returns>Retorna todos os sockets existentes</returns>
    public ConcurrentDictionary<string, WebSocket> GetAll()
    {
      return _sockets;
    }

    /// <summary>
    /// Obter o id de um socket
    /// </summary>
    /// <param name="socket">Socket que queremos obter o id</param>
    /// <returns>Retorna o Id do socket</returns>
    public string GetId(WebSocket socket)
    {
      return _sockets.FirstOrDefault(p => p.Value == socket).Key;
    }

    /// <summary>
    /// Adicionar um socket
    /// </summary>
    /// <param name="username">Nome do user que está a criar o socket</param>
    /// <param name="socket"></param>
    /// <returns></returns>
    public async void AddSocket(string username, WebSocket socket)
    {
      var aux = _sockets.FirstOrDefault(p => p.Key == username);
      if (aux.Value == null)
        _sockets.TryAdd(username, socket);
      else
      {
        try
        {
          await RemoveSocket(aux.Key);
          _sockets.TryAdd(username, socket);
        }
        catch (System.Exception ex)
        {
          if (aux.Value.State == WebSocketState.Open)
          {
            await socket.CloseAsync(closeStatus: WebSocketCloseStatus.NormalClosure,
                            statusDescription: "Fechado pelo gestor de sockets",
                            cancellationToken: CancellationToken.None);
          }
          WebSocket tempSocket;
          _sockets.TryRemove(aux.Key, out tempSocket);
        }
      }
    }


    /// <summary>
    /// Remover um socket
    /// </summary>
    /// <param name="id">Id do socket a remover</param>
    /// <returns></returns>
    public async Task RemoveSocket(string id)
    {
      WebSocket socket;
      _sockets.TryRemove(id, out socket);
    }

    /// <summary>
    /// Verificar se existe conexão
    /// </summary>
    /// <param name="username">Nome do user que está a verificar se existe conexão</param>
    /// <returns>Retorna se existe ou nao conexão</returns>
    public bool CheckIfExistsConnection(string username)
    {
      return _sockets.ContainsKey(username);
    }
  }
}
