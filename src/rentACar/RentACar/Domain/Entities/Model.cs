﻿using Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Model : Entity<Guid>
{
    public Guid BrandId { get; set; }
    public Guid FuelTypeId { get; set; }
    public Guid TransmissionId { get; set; }
    public string Name { get; set; }
    public decimal DailyPrice { get; set; }
    public string ImageUrl { get; set; }


    public virtual Brand? Brand { get; set; }
    public virtual FuelType? FuelType { get; set; }
    public virtual Transmission? Transmission { get; set; }
    public virtual ICollection<Car> Cars { get; set; }
    public Model()
    {
        Cars = new HashSet<Car>();
    }
    public Model(Guid id,Guid brandId,Guid fuelId,Guid transmissionId,string name,decimal dailyPrice,string imageUrl):this()//this() calls other ctor
    {
        Id = id;
        BrandId = brandId;
        FuelTypeId = fuelId;
        TransmissionId = transmissionId;
        Name = name;
        DailyPrice = dailyPrice;
        ImageUrl = imageUrl;

    }

}
