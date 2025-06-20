using System;
using SpecterSDK.API.v2.Players.Me;
using SpecterSDK.APIModels.ClientModels.v2;
using SpecterSDK.Shared;
using SpecterSDK.Shared.v2;

namespace SpecterSDK.ObjectModels.v2
{
    public class SPWalletCurrency : ISpecterPlayerOwnedEntity, ISpecterCurrency
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        public SPResourceType ResourceType => SPResourceType.Currency;
        
        public long Amount => Balance;
        
        public string Code { get; set; }
        public SPCurrencyType Type { get; set; }
        public bool IsVirtual => Type == SPCurrencyType.Virtual;
        public bool IsReal => Type == SPCurrencyType.Real;
        
        public long Balance { get; set; }
        
        public SPWalletCurrency() { }
        public SPWalletCurrency(SPWalletCurrencyData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            
            Rarity = (SPRarity)data.rarity.id;
            Code = data.code;
            Type = data.type;
            
            Balance = (long)data.balance;
        }
    }

    public class SPWalletTransaction
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Status { get; set; }
        
        public SPWalletTransactionCurrencyInfo CurrencyDetails { get; set; }
        
        public SPWalletTransactionResource PurchasedResource { get; set; }
        public SPResourceType PurchasedResourceType => PurchasedResource?.ResourceType ?? SPResourceType.None;
        
        public SPTransactionPurpose Purpose { get; set; }
        public long Amount { get; set; }
        public string Remarks { get; set; }
        
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        
        public SPWalletTransaction() { }
        public SPWalletTransaction(SPWalletTransactionData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Status = data.status;
            
            CurrencyDetails = new SPWalletTransactionCurrencyInfo(data.currencyDetails);
            
            if (data.purchasedItem != null)
                PurchasedResource = new SPWalletTransactionResource(data.purchasedItem, SPResourceType.Item);
            else if (data.purchasedBundle != null)
                PurchasedResource = new SPWalletTransactionResource(data.purchasedBundle, SPResourceType.Bundle);
            
            Purpose = data.purpose.id;
            Amount = (long)data.amount;
            Remarks = data.remarks;
            
            CreatedAt = data.createdAt;
            UpdatedAt = data.updatedAt;
        }
    }

    public class SPWalletTransactionResource : ISpecterEconomyResource
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        
        public SPRarity Rarity { get; set; }
        
        public SPResourceType ResourceType { get; set; }
        
        public SPWalletTransactionResource() { }
        public SPWalletTransactionResource(SPWalletTransactionResourceData data, SPResourceType resourceType)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;

            Rarity = (SPRarity)data.rarity.id;
            ResourceType = resourceType;
        }
    }

    public class SPWalletTransactionCurrencyInfo : ISpecterCurrency
    {
        public string Uuid { get; set; }
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string IconUrl { get; set; }
        public SPRarity Rarity { get; set; }
        
        public string Code { get; set; }
        public SPCurrencyType Type { get; set; }
        public bool IsVirtual => Type == SPCurrencyType.Virtual;
        public bool IsReal => Type == SPCurrencyType.Real;
        
        public SPWalletTransactionCurrencyInfo() { }
        public SPWalletTransactionCurrencyInfo(SPWalletTransactionCurrencyData data)
        {
            Uuid = data.uuid;
            Id = data.id;
            Name = data.name;
            Description = data.description;
            IconUrl = data.iconUrl;
            Rarity = (SPRarity)data.rarity.id;
            Code = data.code;
            Type = data.type;
        }
    }
}