#pragma once

#include <functional>
#include <set>

template <typename T>
class Event
{
private:
    std::set<std::function<void(T)>> listeners;

public:
    void addListener(std::function<void(T)> listener);
    void removeListener(std::function<void(T)> listener);
    void invoke(T data);
};

template <typename T>
void Event<T>::addListener(std::function<void(T)> listener)
{
    listeners.push_back(listener);
}

template <typename T>
void Event<T>::removeListener(std::function<void(T)> listener)
{
    listeners.erase(std::remove(listeners.begin(), listeners.end(), &listener));
}

template <typename T>
void Event<T>::invoke(T data)
{
    std::for_each(listeners.begin(), listeners.end(), []()
                  { this(data); });
}