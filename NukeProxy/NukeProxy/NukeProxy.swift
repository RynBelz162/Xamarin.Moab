//
//  NukeProxy.swift
//  NukeProxy
//
//  Created by Ryan on 5/5/22.
//
//


import Foundation
import UIKit
import Nuke
import NukeExtensions

@objc(ImagePipeline)
public class ImagePipeline : NSObject {
    
    @objc
    public static let shared = ImagePipeline()
    
    @objc
    public func setCacheStrategy(cacheStrategy: DataCachingStrategy, cacheName: String, sizeLimit: Int) -> Void {
        switch cacheStrategy {
        case .withUrlCache:
            Nuke.ImagePipeline.shared = Nuke.ImagePipeline(configuration: .withURLCache)
        case .withDataCache:
            Nuke.ImagePipeline.shared = Nuke.ImagePipeline(configuration: .withDataCache)
            
            let specifiedDataCache = try? Nuke.DataCache(name: cacheName)
            specifiedDataCache?.sizeLimit = sizeLimit
            
            Nuke.ImagePipeline.shared = Nuke.ImagePipeline {
                $0.dataCache = specifiedDataCache
            }
        }
    }
    
    @objc
    public func loadImage(url: URL, onCompleted: @escaping (UIImage?, String) -> Void) {
        
        let request = ImageRequest(url: url)

        _ = Nuke.ImagePipeline.shared.loadImage(
            with: request,
            progress: nil,
            completion: { result in
                switch result {
                case let .success(response):
                    onCompleted(response.image, "success")
                case let .failure(error):
                    onCompleted(nil, error.localizedDescription)
                }
            }
        )
    }
    
    @MainActor @objc
    public func loadImage(url: URL, placeholder: UIImage?, errorImage: UIImage?, into: UIImageView) {
        let options = ImageLoadingOptions(placeholder:placeholder, failureImage: errorImage)
        NukeExtensions.loadImage(with: url, options: options, into: into)
    }
    
    @MainActor @objc
    public func loadImage(url: URL, imageIdKey: String, placeholder: UIImage?, errorImage: UIImage?, into: UIImageView) {
        let options = ImageLoadingOptions(placeholder: placeholder, failureImage: errorImage)
        
        NukeExtensions.loadImage(with: ImageRequest(
            url: url,
            userInfo: [.imageIdKey: imageIdKey]
        ), options: options, into: into)
    }
    
    @objc
    public func loadData(url: URL, onCompleted: @escaping (Data?, URLResponse?) -> Void) {
        loadData(url: url, imageIdKey: nil, reloadIgnoringCachedData: false, onCompleted: onCompleted)
    }
    
    @objc
    public func loadData(url: URL, imageIdKey: String?, reloadIgnoringCachedData: Bool, onCompleted: @escaping (Data?, URLResponse?) -> Void) {
        _ = Nuke.ImagePipeline.shared.loadData(
            with: ImageRequest(
                url: url,
                options: reloadIgnoringCachedData ? [.reloadIgnoringCachedData] : [],
                userInfo: [.imageIdKey: imageIdKey!]
            ),
            completion: { result in
                switch result {
                case let .success(response):
                    onCompleted(response.data, response.response)
                case .failure(_):
                    onCompleted(nil, nil)
                }
            }
        )
    }
}

@objc(ImageCache)
public final class ImageCache: NSObject {
    
    @objc
    public static let shared = ImageCache()
    
    @objc
    public func removeAll() {
        Nuke.ImageCache.shared.removeAll()
    }
}

@objc(DataLoader)
public final class DataLoader: NSObject {
    
    @objc
    public static let shared = DataLoader()
    
    @objc
    public func removeAllCachedResponses() {
        Nuke.DataLoader.sharedUrlCache.removeAllCachedResponses()
    }
}

@objc(Prefetcher)
public final class Prefetcher: NSObject {
 
    let prefetcher = ImagePrefetcher()
    
    @objc
    public func startPrefetching(with: [URL]) {
        prefetcher.startPrefetching(with: with)
    }
    
    @objc
    public func stopPrefetching(with: [URL]) {
        prefetcher.stopPrefetching(with: with)
    }
    
    @objc
    public func stopPrefetching() {
        prefetcher.stopPrefetching()
    }
    
    @objc
    public func pause() {
        prefetcher.isPaused = true
    }
    
    @objc
    public func unPause() {
        prefetcher.isPaused = false
    }
}

@objc (DataCachingStrategy)
public enum DataCachingStrategy: Int {
    case withUrlCache
    case withDataCache
}
