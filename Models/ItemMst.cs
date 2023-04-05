using System;
using System.Collections.Generic;

namespace JewelleryStore.Models;

public partial class ItemMst
{
    public string StyleCode { get; set; }

    public decimal Pairs { get; set; }

    public string BrandId { get; set; }

    public decimal Quantity { get; set; }

    public string CatId { get; set; }

    public string ProdQuality { get; set; }

    public string CertifyId { get; set; }

    public string ProdId { get; set; }

    public string GoldTypeId { get; set; }

    public decimal GoldWt { get; set; }

    public decimal StoneWt { get; set; }

    public decimal NetGold { get; set; }

    public decimal WstgPer { get; set; }

    public decimal Wstg { get; set; }

    public decimal TotGrossWt { get; set; }

    public decimal GoldRate { get; set; }

    public decimal GoldAmt { get; set; }

    public decimal GoldMaking { get; set; }

    public decimal StoneMaking { get; set; }

    public decimal OtherMaking { get; set; }

    public decimal TotMaking { get; set; }

    public decimal Mrp { get; set; }

    public virtual BrandMst Brand { get; set; }

    public virtual CatMst Cat { get; set; }

    public virtual CertifyMst Certify { get; set; }

    public virtual ICollection<DimMst> DimMsts { get; } = new List<DimMst>();

    public virtual GoldKrtMst GoldType { get; set; }

    public virtual ICollection<JewelTypeMst> JewelTypeMsts { get; } = new List<JewelTypeMst>();

    public virtual ProdMst Prod { get; set; }
}
