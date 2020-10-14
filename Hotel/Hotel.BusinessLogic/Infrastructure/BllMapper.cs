// <copyright file="BllMapper.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace Hotel.BusinessLogic.Infrastructure
{
    using AutoMapper;
    using Hotel.BusinessLogic.DTOs;
    using Hotel.BusinessLogic.Models;
    using Hotel.DAL.Entities;

    /// <summary>
    /// Maps objects for Web.
    /// </summary>
    public class BllMapper : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BllMapper"/> class.
        /// </summary>
        public BllMapper()
        {
            this.CreateMap<Room, RoomDTO>()
                .ForMember(
                    dest => dest.RoomClassName,
                    act => act.MapFrom(
                        x => x.RoomClass.Name))
                .ForMember(
                    dest => dest.ClassDisplayColor,
                    act => act.MapFrom(
                        x => x.RoomClass.DisplayColor))
                .ForMember(
                    dest => dest.Price,
                    act => act.MapFrom(
                        x => x.RoomClass.Price * x.PeopleCount));

            this.CreateMap<RoomClass, RoomClassDTO>();

            this.CreateMap<RoomClassDTO, RoomClass>();

            this.CreateMap<Room, RoomWholeInfo>()
                .ForMember(
                    dest => dest.RoomClassInfo,
                    act => act.MapFrom(
                        x => x.RoomClass.Info))
                .ForMember(
                    dest => dest.RoomClassName,
                    act => act.MapFrom(
                        x => x.RoomClass.Name))
                .ForMember(
                    dest => dest.Price,
                    act => act.MapFrom(
                        x => x.RoomClass.Price * x.PeopleCount))
                .ForMember(
                    dest => dest.RoomClassImageAddress,
                    act => act.MapFrom(
                        x => x.RoomClass.AddressImage))
                .ForMember(
                    dest => dest.RoomClassColorToDisplay,
                    act => act.MapFrom(
                        x => x.RoomClass.DisplayColor));

            this.CreateMap<Booking, BookingDTO>()
                .ForMember(
                    dest => dest.RoomClassName,
                    act => act.MapFrom(
                        x => x.Room.RoomClass.Name))
                .ForMember(
                    dest => dest.RoomColorToDisplay,
                    act => act.MapFrom(
                        x => x.Room.RoomClass.DisplayColor))
                .ForMember(
                    dest => dest.RoomNumber,
                    act => act.MapFrom(
                        x => x.Room.Number))
                .ForMember(
                    dest => dest.PeopleCount,
                    act => act.MapFrom(
                        x => x.Room.PeopleCount));
        }
    }
}