#include "Stdafx.h"
#include "MCubeManager.h"
#include "MImageCube.h"
#include <vector>

using namespace System;
using namespace System::Runtime::InteropServices;


CoreWrapper::MCubeManager::MCubeManager()
{
	manager = new CoreNative::CubeManager();
};

CoreWrapper::MCubeManager::~MCubeManager()
{
	delete manager;
};

InterfaceLayer::Image::IImageCube^ CoreWrapper::MCubeManager::GetImageCube(System::Guid imageID)
{
	char* stringPointer = (char*) Marshal::StringToHGlobalAnsi(imageID.ToString()).ToPointer();
	return gcnew CoreWrapper::MImageCube(manager->GetImageCube(stringPointer));
	Marshal::FreeHGlobal(IntPtr(stringPointer));
};

System::Collections::Generic::List<InterfaceLayer::Image::IImageCube^>^ CoreWrapper::MCubeManager::GetAllImageCubes()
{
	System::Collections::Generic::List<InterfaceLayer::Image::IImageCube^>^ list = gcnew System::Collections::Generic::List<InterfaceLayer::Image::IImageCube^>;
	
	std::vector<CoreNative::ImageCube*>* cubes = manager->GetAllImageCubes();
	
	for( std::vector<CoreNative::ImageCube*>::iterator iterator = cubes->begin();
			iterator!= cubes->end(); iterator++)
	{	
		list->Add(gcnew CoreWrapper::MImageCube(*(iterator)));
	}
	return list;
};

void CoreWrapper::MCubeManager::RemoveImageCube(System::Guid imageID)
{
	char* stringPointer = (char*) Marshal::StringToHGlobalAnsi(imageID.ToString()).ToPointer();
	manager->RemoveImageCube(stringPointer);
	Marshal::FreeHGlobal(IntPtr(stringPointer));
};

InterfaceLayer::Image::IImageCube^ CoreWrapper::MCubeManager::CreateImageCube(System::String^ hedFile, System::String^ ctxFile)
{
	char* stringPointerHed = (char*) Marshal::StringToHGlobalAnsi(hedFile).ToPointer();
	char* stringPointerCtx = (char*) Marshal::StringToHGlobalAnsi(ctxFile).ToPointer();
	char* stringPointerGuid = (char*) Marshal::StringToHGlobalAnsi(System::Guid::NewGuid().ToString()).ToPointer();

	InterfaceLayer::Image::IImageCube^ cube = gcnew CoreWrapper::MImageCube(manager->CreateImageCube(stringPointerHed,stringPointerCtx,stringPointerGuid));

	Marshal::FreeHGlobal(IntPtr(stringPointerHed));
	Marshal::FreeHGlobal(IntPtr(stringPointerCtx));
	Marshal::FreeHGlobal(IntPtr(stringPointerGuid));

	return cube;
};

System::Guid CoreWrapper::MCubeManager::GetImageCubeByFilename(System::String^ filename)
{
	char* stringPointer = (char*) Marshal::StringToHGlobalAnsi(filename).ToPointer();
	const char* s = manager->GetImageCubeByFilename(stringPointer);
	if(s != NULL)
	{
		String ^ managedString = Marshal::PtrToStringAnsi((IntPtr) (char*) s);
		Guid^ guid = gcnew Guid(managedString);
		Marshal::FreeCoTaskMem(IntPtr(stringPointer));
		return *guid;
	}
	else
	{
		return Guid::Empty;
	}
};