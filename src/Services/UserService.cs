using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using CodeCrafters_backend_teamwork.src.Abstractions;
using CodeCrafters_backend_teamwork.src.Databases;
using CodeCrafters_backend_teamwork.src.DTOs;
using CodeCrafters_backend_teamwork.src.Entities;
using CodeCrafters_backend_teamwork.src.Utility;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace CodeCrafters_backend_teamwork.src.Services;

public class UserService : IUserService
{
    private IUserRepository _userRepository;
    private IConfiguration _config;
    private IMapper _mapper;

    public UserService(IUserRepository userRepository, IConfiguration config, IMapper mapper)
    {
        _userRepository = userRepository;
        _config = config;
        _mapper = mapper;
    }


    public IEnumerable<UserReadDto> FindMany()
    {
        var users = _userRepository.FindMany();
        var usersRead = users.Select(_mapper.Map<UserReadDto>);
        return usersRead.ToList();
    }
    // public UserReadDto? CreateOneTest(UserCreateDto user)
    // {
    //     var mappedUser = _mapper.Map<User>(user);

    //     var foundUser = _userRepository.FindOneByEmail(mappedUser.Email);
    //     Console.WriteLine($"Create One method triggers");

    //     if (foundUser is not null)
    //     {
    //         Console.WriteLine($"user email is exist in database");
    //         return null;
    //     }

    //     var userCreated = _userRepository.CreateOne(mappedUser);
    //     return _mapper.Map<UserReadDto>(userCreated);
    // }

    public string? SignIn(UserSignIn userSign)
    {
        User? user = _userRepository.FindOneByEmail(userSign.Email);
        if (user is null)
        {
            return null;
        }
        byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt:Pepper"]!);

        bool isCorrectPass = PasswordUtils.VerifyPassword(userSign.Password, user.Password, pepper);
        if (!isCorrectPass) return null;
        
        // the auth code here 
        var claims = new[] 
        {
            new Claim(ClaimTypes.Name, user.FirstName),
             new Claim(ClaimTypes.Role, user.Role.ToString()),
              new Claim(ClaimTypes.Email, user.Email),
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SigningKey"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.Now.AddDays(7),
            signingCredentials: creds
        );

        var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
        return tokenString;
    }

    public UserReadDto? SignUp(UserCreateDto user)
    {
        var foundUser = _userRepository.FindOneByEmail(user.Email);

        if (foundUser is not null)
        {
            Console.WriteLine($"item is found");
            return null;
        }

        byte[] pepper = Encoding.UTF8.GetBytes(_config["Jwt:Pepper"]!);

        PasswordUtils.HashPassword(user.Password, out string hashedPassword,
        pepper);

        user.Password = hashedPassword;
        User mappedUser = _mapper.Map<User>(user);
        User newUser = _userRepository.CreateOne(mappedUser);
        UserReadDto userRead = _mapper.Map<UserReadDto>(newUser);

        return userRead;
    }

    public UserReadDto? FindOneByEmail(string email)
    {
        User? user = _userRepository.FindOneByEmail(email);
        UserReadDto? usersRead = _mapper.Map<UserReadDto>(user);
        return usersRead;

    }

    public User? UpdateOne(string email, User newValue)
    {
        User? user = _userRepository.FindOneByEmail(email);
        if (user is not null)
        {
            user.FirstName = newValue.FirstName;
            return _userRepository.UpdateOne(user);
        }
        return null;
    }

    public IEnumerable<UserReadDto> DeleteOne(Guid userId)
    {
        return (IEnumerable<UserReadDto>)_userRepository.DeleteOne(userId);
    }

    public User FindOne(Guid userId)
    {
        throw new NotImplementedException();
    }


    IEnumerable<User> IUserService.DeleteOne(Guid id)
    {
        throw new NotImplementedException();
    }

    UserReadDto IUserService.FindOne(Guid id)
    {
        throw new NotImplementedException();
    }

    string IUserService.SignIn(UserSignIn user)
    {
        throw new NotImplementedException();
    }
}
