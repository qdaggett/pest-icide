#pragma once

#ifdef DYNAMICSOUNDASSIGNMENT_EXPORTS
#define LIB_API __declspec(dllexport)
#elif DYNAMICSOUNDASSIGNMENT_IMPORTS
#define LIB_API __declspec(dllimport)
#else
#define LIB_API
#endif
