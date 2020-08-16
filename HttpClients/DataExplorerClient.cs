using Delivery1.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace Delivery1.HttpClients
{
    public class AClient
    {
        private readonly HttpClient client;

        public AClient(HttpClient client)
        {
            client.BaseAddress = new Uri("https://jsonplaceholder.typicode.com");

            this.client = client;
        }

        private IEnumerable<T> GetEntity<T>(string entityName)
            => client
                    .GetFromJsonAsync<IEnumerable<T>>(entityName)
                    .GetAwaiter().GetResult();

        public IEnumerable<Album> GetAlbums(CurrentState state)
            => GetEntity<Album>("albums").Where(i => i.UserId == state.UserId);

        public IEnumerable<Photo> GetPhotos(CurrentState state)
            => GetEntity<Photo>("photos").Where(i => i.AlbumId == state.AlbumId);

        public IEnumerable<Comment> GetComments(int photoId)
            => GetEntity<Comment>("comments").Where(i => i.PostId == photoId);            
    }
}
