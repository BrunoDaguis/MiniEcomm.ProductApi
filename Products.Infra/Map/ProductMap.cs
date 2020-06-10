using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using Products.Domain.Entities;

namespace Products.Infra.Map
{
    public class ProductMap
    {
        public static void Configure()
        {
            BsonClassMap.RegisterClassMap<ProductEntity>(map =>
            {

                map.AutoMap();
                map.SetIgnoreExtraElements(true);
                map.MapIdField(x => x.Id);
                map.MapMember(x => x.Name).SetIsRequired(true);
                map.MapMember(x => x.Price).SetIsRequired(true);
                map.MapMember(x => x.Deleted).SetIsRequired(true).SetDefaultValue(false);
            });
        }
    }
}