//MCubeManager
#include "Stdafx.h"
#include "..\CoreNative\CubeManager.h"

using namespace System;

namespace CoreWrapper
{
	public ref class MCubeManager : public InterfaceLayer::ICubeManager
	{
		private:
			CoreNative::CubeManager* manager;

		public:
			MCubeManager();
			~MCubeManager();
			virtual InterfaceLayer::Image::IImageCube^ GetImageCube(System::Guid imageID) = InterfaceLayer::ICubeManager::GetImageCube;
			virtual System::Collections::Generic::List<InterfaceLayer::Image::IImageCube^>^ GetAllImageCubes() = InterfaceLayer::ICubeManager::GetAllImageCubes;
			virtual void RemoveImageCube(System::Guid imageID) = InterfaceLayer::ICubeManager::RemoveImageCube;
			virtual InterfaceLayer::Image::IImageCube^ CreateImageCube(System::String^ hedFile, System::String^ ctxFile) = InterfaceLayer::ICubeManager::CreateImageCube;
			virtual System::Guid GetImageCubeByFilename(System::String^ filename) = InterfaceLayer::ICubeManager::GetImageCubeByFilename;
	};
};
